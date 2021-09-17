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
              <v-my-text-field v-model="row.code"></v-my-text-field>
              <v-my-text-field v-model="row.name"></v-my-text-field>
              <v-my-select-sng v-model="row.projectId" :items="projects"></v-my-select-sng>
              <v-my-text-field v-model="row.framework"></v-my-text-field>
              <v-my-text-field v-model="row.owner"></v-my-text-field>
              <v-my-text-field v-model="row.repositoryUrl"></v-my-text-field>
              <v-my-textarea v-model="row.buildProcessUrls"></v-my-textarea>
              <v-my-textarea v-model="row.description"></v-my-textarea>
            </v-card-text>
          </v-card>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <v-card>
            <v-section-toolbar caption="applications.endpointsHeader">
              <template slot="startButtons">
                <v-tooltip right>
                  <template v-slot:activator="{ on, attrs }">
                    <v-btn
                           dark
                           small
                           fab
                           color="success"
                           class="mr-2"
                           @click="addEndpoint">

                      <v-icon v-bind="attrs" v-on="on">mdi-plus</v-icon>
                    </v-btn>
                  </template>
                  <span>{{ $t('common.buttons.addRow') }}</span>
                </v-tooltip>
              </template>
            </v-section-toolbar>

            <v-in-memory-list-data-source :items="row.endpoints.value" :is-error="isError" :is-loading="isLoading">
              <template slot-scope="{ ds }">
                <v-my-data-table
                    :headers="applicationEndpointsHeaders"
                    :items="ds.filteredItems"
                    :loading="ds.isLoading"
                    :server-items-length="ds.totalItems"
                    :options.sync="ds.options"
                    sort-by="null"
                >
                  <template v-slot:item.actions="{ item }">

                    <v-tooltip right>
                      <template v-slot:activator="{ on, attrs }">
                        <v-icon class="error--text" v-bind="attrs" v-on="on" @click="removeEndpoint(item)">mdi-delete
                        </v-icon>
                      </template>
                      <span>{{ $t('common.buttons.delete') }}</span>
                    </v-tooltip>
                  </template>
                  <template v-slot:item.environmentId="{ item, header }">
                    <v-select
                        :items="environments"
                        item-text="id"
                        item-value="id"
                        v-model="item[header.value]"
                        :rules="header.validationRules"
                    ></v-select>

                  </template>
                  <template v-slot:item.path="{ item, header }">
                    <v-text-field
                        v-model="item[header.value]"
                        :rules="header.validationRules"
                    ></v-text-field>
                  </template>
                  <template v-slot:item.comment="{ item, header }">
                    <v-text-field
                        v-model="item[header.value]"
                    ></v-text-field>
                  </template>

                  <template v-slot:no-data v-if="isError">
                    <div v-if="isError">{{ $t('common.errorMessage') }}</div>
                  </template>
                </v-my-data-table>
              </template>
            </v-in-memory-list-data-source>
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
              >Save changes
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
import ApplicationModel from "@/views/applications/ApplicationFormModel";
import {ValidationRules} from "@/validation/ValidationRules";

export default Vue.extend({
  data() {
    return {
      captionTranslationKey: "applications.editHeader",
      projects: [],
      environments: [],
      valid: false,
      isError: false,
      isLoading: true,
      isServerValidationError: false,
      serverValidationErrors: [],
      row: new ApplicationModel(),
      item: {} as any,
      applicationEndpointsHeaders: [
        {
          text: this.$t("common.fieldNames.actions"),
          value: "actions",
          sortable: false,
          class: "actions",
          filterable: false,
          width: 100,
        },
        {
          text: "Environment",
          value: "environmentId",
          groupable: false,
          filterable: true,
          validationRules: [ValidationRules.required()],
        },
        {text: "Path", value: "path", groupable: false, validationRules: [ValidationRules.required()],},
        {text: "Comment", value: "comment", groupable: false},
      ]
    };
  },

  computed: {
    caption(): string {
      return this.$t(this.captionTranslationKey, this.item).toString();
    },
    goBackUrl(): string {
      const source = this.$route.query.source || "details";
      if (source === "list")
        return "/applications";
      else
        return `/applications/${this.$route.params.id}/details`;

    }
  },
  async mounted() {
    this.loadData();

    try {

      const [projectsResponse, environmentsResponse] = await Promise.all([
        HttpClient.getProjects(),
        HttpClient.getEnvironments()
      ]);

      this.projects = projectsResponse.data.items;
      this.environments = environmentsResponse.data.items;
    } catch (error) {
      this.isError = true;
    } finally {
      this.isLoading = false;
    }
  },
  methods: {
    loadData() {
      this.isLoading = true;
      const id: string = this.$route.params.id;
      HttpClient.getApplicationDetails(id)
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

      const newApp = form.serializeRow(this.row);
      newApp.id = this.$route.params.id;
      HttpClient.updateApplication(newApp)
          .then((creationResult) => {
            if (creationResult.status === 200) {
              this.$router.push(
                  `/applications/${creationResult.data.id}/details`
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
      (this.$refs.form as any).resetValidation();
      const item = this.item;
      const row = this.row;

      this.$nextTick(() => {
        for (const key in item) {
          const val = item[key];
          const rowItem = row[key] as form.RowItem;
          if (rowItem) {
            rowItem.value = val;
          }
        }
      });

    },
    removeEndpoint(endpoint) {
      const endpoints = this.item.endpoints;

      const index = endpoints.indexOf(endpoint, 0);
      if (index > -1) {
        endpoints.splice(index, 1);
      }
    },
    addEndpoint() {
      const endpoints = this.item.endpoints;
      endpoints.push({});
      const form = (this.$refs.form as any);
      Vue.nextTick()
          .then(function () {
            form.validate();
          })

    },
  },
});
</script>
