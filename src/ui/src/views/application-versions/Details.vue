<template>
  <v-container>
    <v-view-details-header :isError="isError" :isLoading="isLoading"></v-view-details-header>
    <v-view-details-toolbar v-if="dataLoaded" :caption="caption" :goBackUrl="goBackUrl">
    </v-view-details-toolbar>

    <v-row v-if="dataLoaded">
      <v-col>
        <v-card>
          <v-section-toolbar caption="common.sectionNames.general"></v-section-toolbar>
          <v-card-text>
            <v-view-details-row label="Id" :value="item.id"></v-view-details-row>
            <v-view-details-row label="Application" :value="item.applicationName"></v-view-details-row>
            <v-view-details-row label="Version" :value="item.version"></v-view-details-row>
            <v-view-details-row label="Environment" :value="item.environmentId"></v-view-details-row>
            <v-view-details-row label="Framework version" :value="item.frameworkVersion"></v-view-details-row>
            <v-view-details-row label="Tools version" :value="item.toolsVersion"></v-view-details-row>
            <v-view-details-row
                :label="$t('common.fieldNames.createDate')"
                :value="$formatDateTime(item.createDate)"
            ></v-view-details-row>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
    <v-row v-if="dataLoaded">
      <v-col>
        <v-card>
          <v-section-toolbar caption="applicationVersionDependencies.specificationsHeader">
            <template slot="startButtons">
              <v-dialog
                  v-model="dialog"
                  width="500"
              >
                <template v-slot:activator="{ on, attrs }">
                  <v-btn
                      dark
                      small
                      fab
                      color="success"
                      class="mr-2">
                    <v-icon v-bind="attrs" v-on="on">mdi-plus</v-icon>
                  </v-btn>
                </template>

                <v-card>
                  <v-toolbar color="primary" dark>
                    Add new specification to the application version
                  </v-toolbar>
                  <br>
                  <v-card-text>
                    <div>
                      <v-btn dark small fab color="success" class="mr-2">
                        <v-icon>mdi-plus</v-icon>
                      </v-btn>
                      <span>Swagger specification</span>
                    </div>
                  </v-card-text>
                  <v-divider></v-divider>
                  <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn color="primary" text @click="dialog = false">
                      Close
                    </v-btn>
                  </v-card-actions>
                </v-card>
              </v-dialog>
            </template>
          </v-section-toolbar>
          <v-ajax-list-data-source :httpPath="applicationVersionSpecificationsPath">
            <template slot-scope="{ ds }">
              <v-my-data-table
                  :headers="applicationSpecificationsHeaders"
                  :items="ds.filteredItems"
                  :loading="ds.isLoading"
                  :server-items-length="ds.totalItems"
                  :options.sync="ds.options"
                  multi-sort
                  sort-by="['type', 'name']"
                  :show-group-by="true"
              >
                <template v-slot:body.prepend="{ headers }">
                  <v-my-data-table-search-row :ds="ds" :headers="headers"/>
                </template>
                <template v-slot:item.actions="{ item }">
                  <router-link
                      :to="`/applications/${$data.item.applicationId}/application-versions/${$data.item.id}/swagger/${item.id}`"
                      class="action-link mr-4">
                    <v-btn color="success" outlined>
                      <img src="@/assets/swagger.png" alt="Swagger"
                           style="width: 17px; vertical-align: middle; margin-right: 5px;"/>
                      Swagger
                    </v-btn>
                  </router-link>
                </template>

                <template v-slot:item.environmentId="{ item }">
                  <v-column-link-env :env="item.environmentId"></v-column-link-env>
                </template>
                <template v-slot:item.path="{ item }">
                  <a :href="item.path">{{ item.path }}</a>
                </template>
                <template v-slot:no-data v-if="isError">
                  <div v-if="isError">{{ $t('common.errorMessage') }}</div>
                </template>
              </v-my-data-table>
            </template>
          </v-ajax-list-data-source>
        </v-card>
      </v-col>
    </v-row>
    <!--    <v-row v-if="dataLoaded">-->
    <!--      <v-col>-->
    <!--        <v-card>-->
    <!--          <v-section-toolbar caption="applicationVersionDependencies.dependenciesHeader"></v-section-toolbar>-->
    <!--          <v-tabs vertical>-->
    <!--            <v-tab class="text-left">-->
    <!--              <v-icon left>-->
    <!--                mdi-application-->
    <!--              </v-icon>-->
    <!--              Applications-->
    <!--            </v-tab>-->
    <!--            <v-tab class="text-left">-->
    <!--              <span>-->
    <!--                <img src="@/assets/NuGet-Logo.svg" alt="Nuget" style="width: 25px; vertical-align: middle"/>-->
    <!--                Nugets-->
    <!--              </span>-->
    <!--            </v-tab>-->

    <!--            <v-tab-item>-->
    <!--              <application-dependencies></application-dependencies>-->
    <!--            </v-tab-item>-->

    <!--            <v-tab-item>-->
    <!--              <nuget-dependencies></nuget-dependencies>-->
    <!--            </v-tab-item>-->

    <!--          </v-tabs>-->
    <!--        </v-card>-->
    <!--      </v-col>-->
    <!--    </v-row>-->

    <!--    <v-row>-->
    <!--      <v-col>-->
    <!--        <v-card>-->
    <!--          <v-section-toolbar caption="applicationVersionDependencies.endpointsHeader"></v-section-toolbar>-->
    <!--          <endpoints :application-version-id="id" :environment="item.environmentId"></endpoints>-->
    <!--        </v-card>-->
    <!--      </v-col>-->
    <!--    </v-row>-->
  </v-container>
</template>

<script lang="ts">
import Vue from "vue";
import {HttpClient} from "@/services/httpClient/HttpClient";
import NugetDependencies from './dependencies-views/NugetDependencies.vue';
import ApplicationDependencies from "@/views/application-versions/dependencies-views/ApplicationDependencies.vue";
import Endpoints from "@/views/application-versions/_Endpoints.vue";

interface ApplicationVersionDetails {
  id: string;
  applicationId: string;
  applicationName: string;
  version: string;
  environmentId: string;
  frameworkVersion: string;
  toolsVersion: string;
  createDate: string;
}

export default Vue.extend({
  data() {
    return {
      dialog: false,
      id: this.$route.params.id,
      isLoading: true,
      isError: false,
      item: {} as ApplicationVersionDetails,
      captionTranslationKey: "applicationVersions.detailsHeader",
      applicationSpecificationsHeaders: [
        {
          text: "Actions",
          value: "actions",
          sortable: false,
          class: "actions",
          filterable: false,
          groupable: false,
        },
        {text: "Type", value: "type", groupable: true, filterable: true,},
        {text: "Name", value: "name", groupable: false, filterable: true,},
        // {text: "Create date", value: "createDate", groupable: false, filterable: false},
      ]
    };
  },

  async mounted() {
    await this.loadData();
  },
  computed: {
    dataLoaded(): boolean {
      return !this.isError && !this.isLoading;
    },
    caption(): string {
      return this.$t(this.captionTranslationKey, this.item).toString();
    },
    goBackUrl(): string {
      return `/applications/${this.item.applicationId}/details`
    },
    applicationVersionSpecificationsPath(): string {
      return HttpClient.getApplicationVersionSpecificationsPath(this.item.id);
    }
  },
  methods: {
    async loadData() {
      this.isLoading = true;

      HttpClient.getApplicationVersionDetails(this.id)
          .then((response) => {
            this.item = response.data;
          })
          .catch(() => {
            this.isError = true;
          })
          .finally(() => {
            this.isLoading = false;
          });
    },
    goBack() {
      this.$router.push("/env");
    },
  },
  components: {
    //NugetDependencies,
    //ApplicationDependencies,
    //Endpoints,
  },
});
</script>
<style type="text/css">
a[disabled] {
  pointer-events: none;
  cursor: crosshair !important;
  /*cursor: default !important;*/
}
</style>