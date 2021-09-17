<template>
  <v-container>
    <v-create-header :isError="isError" :isLoading="false"></v-create-header>
    <v-create-toolbar v-if="!isError" :caption="caption" :goBackUrl="goBackUrl"></v-create-toolbar>

    <v-form
        ref="form"
        v-model="valid"
        :lazy-validation="false"
        v-if="!isError"
        :disabled="isLoading"
    >
      <v-row>
        <v-col>
          <v-card :loading="isLoading">
            <v-section-toolbar caption="common.sectionNames.general"></v-section-toolbar>
            <v-card-text>
              <v-my-select-sng v-model="row.applicationId" :items="applications"></v-my-select-sng>
              <v-my-select-sng v-model="row.environmentId" :items="environments"></v-my-select-sng>
              <v-my-text-field v-model="row.version"></v-my-text-field>
              <v-my-text-field v-model="row.frameworkVersion"></v-my-text-field>
            </v-card-text>
          </v-card>
        </v-col>
      </v-row>
      <v-row v-if="isServerValidationError && !isError">
        <v-col>
          <v-card color="error" dense style="color: white">
            <v-card-text style="color: white">
              <v-icon style="color: white">mdi-alert</v-icon>
              <span>Some validation errors found. Fix it first.</span>
            </v-card-text>
          </v-card>
        </v-col>
      </v-row>
      <v-row v-if="!isError">
        <v-col>
          <v-card>
            <v-card-text>
              <v-btn
                  :disabled="!valid || isLoading"
                  color="success"
                  class="mr-4"
                  @click="create"
              >Create
              </v-btn>
              <v-btn :disabled="isLoading" color="error" class="mr-4" @click="reset">Reset Form</v-btn>
            </v-card-text>
          </v-card>
        </v-col>
      </v-row>
    </v-form>
  </v-container>
</template>

<script lang="ts">
import Vue from "vue";
import {HttpClient} from "@/services/httpClient/HttpClient";
import * as form from "@/services/formHelpers/FormHelpers";
import {ValidationRules} from "@/validation/ValidationRules";

export default Vue.extend({
  data() {
    return {
      goBackUrl: `/applications/${this.$route.params.applicationId}/details`,
      captionTranslationKey: "applicationVersions.createHeader",
      applications: [],
      environments: [],
      valid: false,
      isError: false,
      isLoading: true,
      isServerValidationError: false,
      serverValidationErrors: [],
      row: {
        frameworkVersion: new form.RowItem({
          label: "Framework version",
          validationRules: [
            // ValidationRules.required(),
            // ValidationRules.maxLength(25),
          ],
        }),

        version: new form.RowItem({
          label: "Version",
          validationRules: [
            ValidationRules.required(),
            ValidationRules.maxLength(25),
          ],
        }),
        environmentId: new form.RowItem({
          label: "Environment",
          validationRules: [ValidationRules.required()],
          itemText: "id",
          itemValue: "id",
        }),
        applicationId: new form.RowItem({
          label: "Application",
          validationRules: [ValidationRules.required()],
          itemText: "name",
          itemValue: "id",
        }),
      },
    };
  },
  computed: {
    caption(): string {
      return this.$t(this.captionTranslationKey).toString();
    },
  },
  async mounted() {
    try {
      this.row.applicationId.value = this.$route.params.applicationId;
      const [responseEnvironments, responseApplications] = await Promise.all([
        HttpClient.getEnvironments(),
        HttpClient.getApplications(),
      ]);

      this.applications = responseApplications.data.items;
      this.environments = responseEnvironments.data.items;
    } catch (error) {
      this.isError = true;
    } finally {
      this.isLoading = false;
    }
  },
  methods: {
    create() {
      this.isLoading = true;
      // eslint-disable-next-line
      (this.$refs.form as any).validate();

      const newApp = form.serializeRow(this.row);
      const applicationId = this.row.applicationId;
      HttpClient.createApplicationVersion(newApp)
          .then((creationResult) => {
            if (creationResult.status === 200) {
              this.$router.push(
                  `/applications/${applicationId}/application-versions/${creationResult.data.id}/details`
              );
            }
          })
          .catch((errors) => {
            const errorCode = errors.response.status;
            const row = this.row;
            switch (errorCode) {
              case 422:
                form.processErrors(errors, row);
                this.isServerValidationError = true;
                break;
              default:
                this.isError = true;
            }
          })
          .finally(() => {
            this.isLoading = false;
          });
    },
    reset() {
      // eslint-disable-next-line
      (this.$refs.form as any).reset();
    },
  },
});
</script>