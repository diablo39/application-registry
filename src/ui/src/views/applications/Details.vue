<template>
  <v-container>
    <v-view-details-header :isError="isError" :isLoading="isLoading"></v-view-details-header>
    <v-view-details-toolbar v-if="dataLoaded" :caption="caption" :goBackUrl="goBackUrl">
      <template slot="endButtons">
        <router-link :to="`/applications/${application.id}/application-versions/create`" class="action-link mr-4">
          <v-btn color="success">
            <v-icon>mdi-plus</v-icon>
            Add version
          </v-btn>
        </router-link>

        <router-link :to="`/applications/${application.id}/edit`" class="action-link">
          <v-btn color="#1E88E5" dark>
            <v-icon>mdi-pencil</v-icon>
            Edit
          </v-btn>
        </router-link>
      </template>
    </v-view-details-toolbar>

    <v-row v-if="dataLoaded">
      <v-col>
        <v-card>
          <v-section-toolbar caption="common.sectionNames.general"></v-section-toolbar>
          <v-card-text>
            <v-view-details-row label="Name" :value="application.name"></v-view-details-row>
            <v-view-details-row label="Code" :value="application.code"></v-view-details-row>
            <v-view-details-row
                label="System name"
                :value="application.projectName"
                :to="'/systems/' + application.projectId"
            ></v-view-details-row>
            <v-view-details-row label="Framework" :value="application.framework"></v-view-details-row>
            <v-view-details-row label="Owner" :value="application.owner"></v-view-details-row>
            <v-view-details-row label="Repository url" :value="application.repositoryUrl"></v-view-details-row>
            <v-view-details-row label="Build Process Urls" :value="application.buildProcessUrls"></v-view-details-row>

            <v-view-details-row label="Description" :value="application.description"></v-view-details-row>
            <v-view-details-row
                label="Create date"
                :value="$formatDateTime(application.createDate)"
            ></v-view-details-row>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
    <v-row v-if="dataLoaded">
      <v-col>
        <v-card>
          <v-section-toolbar caption="applications.endpointsHeader"></v-section-toolbar>
          <v-in-memory-list-data-source :items="application.endpoints" :is-error="isError" :is-loading="isLoading">
            <template slot-scope="{ ds }">
              <v-my-data-table
                  :headers="applicationEndpointsHeaders"
                  :items="ds.filteredItems"
                  :loading="ds.isLoading"
                  :server-items-length="ds.totalItems"
                  :options.sync="ds.options"
                  sort-by="environmentId"
              >
                <template v-slot:body.prepend="{ headers }">
                  <v-my-data-table-search-row :ds="ds" :headers="headers"/>
                </template>
                <template v-slot:item.environmentId="{ item }">
                  <v-column-link-env :env="item.environmentId"></v-column-link-env>
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

    <v-row v-if="dataLoaded">
      <v-col>
        <v-card>
          <v-section-toolbar caption="applications.versionsHeader"></v-section-toolbar>
          <v-ajax-list-data-source :httpPath="httpPath">
            <template slot-scope="{ ds }">
              <v-my-data-table
                  :headers="applicationVersionsHeaders"
                  :items="ds.filteredItems"
                  :loading="ds.isLoading"
                  :server-items-length="ds.totalItems"
                  :options.sync="ds.options"
                  sort-by="idEnvironment"
                  :show-group-by="true"

              >
                <template v-slot:body.prepend="{ headers }">
                  <v-my-data-table-search-row :ds="ds" :headers="headers"/>
                </template>
                <template v-slot:item.idEnvironment="{ item }">
                  <v-column-link-env :env="item.idEnvironment"></v-column-link-env>
                </template>
                <template v-slot:item.actions="{ item }">
                  <router-link
                      :to="paths.getApplicationVersionDetails(application.id,item.id)"
                      title="Details"
                      class="action-link"
                  >
                    <v-icon class="success--text">mdi-details</v-icon>
                  </router-link>
                  <router-link
                      v-if="item.hasSwaggerSpecification"
                      :to="`/applications/${application.id}/application-versions/${item.id}/swagger`"
                      title="Details"
                      class="action-link"
                  >
                    <img src="@/assets/swagger.png" alt="Swagger" style="width: 17px; vertical-align: middle"/>
                  </router-link>
                </template>
                <template v-slot:item.collectorExecutionSucceeded="{ item }">
                  <v-dialog
                      max-width="500"
                  >
                    <template v-slot:activator="{ on, attrs }">

                      <v-chip
                          v-bind="attrs"
                          v-on="on"
                          :color="getColor(item.collectorExecutionSucceeded)"
                          dark
                      >
                        {{ item.collectorExecutionSucceeded ? "Success" : "Failed" }}
                      </v-chip>
                    </template>
                    <v-card>
                      <v-card-title class="headline">
                        Collector details
                      </v-card-title>
                      <v-card-text>Collector execution duration [s]: {{item.collectorExecutionDuration}}</v-card-text>
                      <v-card-text>Collector batches statuses:</v-card-text>
                      <v-card-text>
                        <pre>{{item.collectorBatchStatuses}}</pre>
                      </v-card-text>

                    </v-card>
                  </v-dialog>

                </template>
                <template v-slot:item.createDate="{ item }">{{ $formatDateTime(item.createDate) }}</template>
                <template v-slot:no-data v-if="ds.isError">
                  <div v-if="isError">{{ $t('common.errorMessage') }}</div>
                </template>
              </v-my-data-table>
            </template>
          </v-ajax-list-data-source>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import Vue from "vue";
import {HttpClient} from "@/services/httpClient/HttpClient";
import Paths from '@/router/Paths';

export default Vue.extend({
  data() {
    return {
      id: this.$route.params.id,
      httpPath: HttpClient.getApplicationVersionsPath(this.$route.params.id),
      captionTranslationKey: "applications.detailsHeader",
      goBackUrl: "/applications",
      isLoading: true,
      isError: false,
      application: {},
      applicationVersionsHeaders: [
        {
          text: "Actions",
          value: "actions",
          sortable: false,
          class: "actions",
          filterable: false,
          groupable: false,
        },
        {text: "Environment", value: "idEnvironment", groupable: true, filterable: true,},
        {text: "Version", value: "version", groupable: false},
        {text: "Collector status", value: "collectorExecutionSucceeded", groupable: false},
        {text: "Tools version", value: "toolsVersion", groupable: false},
        {text: "Create date", value: "createDate", groupable: false, filterable: false},
      ],
      applicationEndpointsHeaders:[
        {text: "Environment", value: "environmentId", groupable: true, filterable: true,},
        {text: "Path", value: "path", groupable: false},
        {text: "Comment", value: "comment", groupable: false},
      ]

    };
  },
  computed: {
    dataLoaded(): boolean {
      return !this.isError && !this.isLoading;
    },
    caption(): string {
      return this.$t(this.captionTranslationKey, this.application).toString();
    },
    paths(): any {
      return Paths;
    },

  },
  async mounted() {
    try {
      const id: string = this.$route.params.id;
      const [response] = await Promise.all([
        HttpClient.getApplicationDetails(id),
      ]);

      this.application = response.data;
    } catch (error) {
      this.isError = true;
    } finally {
      this.isLoading = false;
    }
  },
  methods: {
    getColor (collectorExecutionSucceeded) {
      if (collectorExecutionSucceeded) return 'green'
      else return 'red'
    },

  },
});
</script>