<template>
  <v-container>
    <v-edit-header :isError="isError" :isLoading="false"></v-edit-header>
    <v-edit-toolbar v-if="!isError" :caption="caption" :goBackUrl="goBackUrl"></v-edit-toolbar>

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
              <v-my-text-field v-model="row.cidr"></v-my-text-field>
              <v-my-text-field v-model="row.name"></v-my-text-field>
              <v-my-text-field v-model="row.alias"></v-my-text-field>
              <v-my-number-field v-model="row.number"></v-my-number-field>
              <v-my-text-field v-model="row.rfc"></v-my-text-field>
              <v-my-textarea v-model="row.description"></v-my-textarea>
              <v-my-checkbox-field v-model="row.isVirtualDirectory"></v-my-checkbox-field>
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
                  @click="update"
              >Save changes</v-btn>
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
import { ValidationRules } from "@/validation/ValidationRules";
import { HttpClient } from "@/services/httpClient/HttpClient";
import * as form from "@/services/formHelpers/FormHelpers";
import Paths from "@/router/Paths";

declare interface ItemModel {
  id: string;
  name: string;
  description: string;
}

export default Vue.extend({
  data() {
    return {
      captionTranslationKey: "vlans.editHeader",

      valid: false,
      isError: false,
      isLoading: false,
      isServerValidationError: false,
      serverValidationErrors: [],
      item: {} as ItemModel,
      row: {
        id: new form.RowItem({
          label: "Id",
          readonly: true,
          validationRules: [
          //  ValidationRules.required(),
       //     ValidationRules.maxLength(25),
          ],
        }),
        cidr: new form.RowItem({
          label: "CIDR",
          validationRules: [
            ValidationRules.required(),
            ValidationRules.maxLength(40),
          ],
        }),
        name: new form.RowItem({
          label: "Name",
          validationRules: [
            ValidationRules.required(),
            ValidationRules.maxLength(400),
          ],
        }),
        alias: new form.RowItem({
          label: "Alias",
          validationRules: [
            ValidationRules.maxLength(400),
          ],
        }),
        number: new form.RowItem({
          label: "Number",
          validationRules: [
            ValidationRules.lessThen(65000),
          ],
        }),
        rfc: new form.RowItem({
          label: "RFC",
          validationRules: [
            ValidationRules.maxLength(400),
          ],
        }),
        description: new form.RowItem({
          label: "Description",
          validationRules: [
            ValidationRules.maxLength(1200),
          ],
        }),
        isVirtualDirectory: new form.RowItem({
          label: "Is virtual directory",
          validationRules: [
          ],
        }),
      },
    };
  },
  mounted() {
    this.loadData();
  },
  computed: {
    caption(): string {
      return this.$t(this.captionTranslationKey).toString();
    },
    goBackUrl(): string {
      const source = this.$route.query.source || "details";
      const id: string = this.$route.params.id;
      if (source === "list") return "/vlans";
      else return Paths.getVlanDetails(id);
    },
  },
  methods: {
    loadData() {
      this.isLoading = true;
      const id: string = this.$route.params.id;
      HttpClient.getVlan(id)
          .then((response) => {
            this.item = response.data;

            this.reset();
          })
          .catch(() => {
            this.isError = true;
          })
          .finally(() => {
            this.isLoading = false;
          });
    },
    update() {
      this.isLoading = true;
      // eslint-disable-next-line
      (this.$refs.form as any).validate();

      const id = this.row.id.value;
      const item = form.serializeRow(this.row);

      HttpClient.updateVlan(item)
          .then((creationResult) => {
            if (creationResult.status === 200) {
              this.$router.push(Paths.getVlanDetails(id));
            }
          })
          .catch((errors) => {
            const errorCode = errors.response.status;

            switch (errorCode) {
              case 422:
                for (let errorField in errors.response.data.errors) {
                  const errorMessages = errors.response.data.errors[errorField];
                  if (errorField) {
                    errorField =
                        errorField[0].toLowerCase() +
                        (errorField.length > 1 ? errorField.substring(1) : "");
                    this.row[errorField].errorMessages = errorMessages;
                  }
                }
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
      //const that = this;
      window.setTimeout(() => {
        for(const prop in this.row) {
          if(this.item[prop]) {
            this.row[prop].value = this.item[prop];
          }
        }
      }, 0);
    },
  },
});
</script>