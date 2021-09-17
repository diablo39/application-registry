<template>
  <v-container>
    <v-view-details-toolbar :caption="caption" :goBackUrl="goBackUrl"></v-view-details-toolbar>
    
    <v-row>
      <v-col>
        <v-card>
          <v-section-toolbar caption="nugetPackages.versionsHeader"></v-section-toolbar>
          <v-ajax-list-data-source :httpPath="httpPath">
            <template slot-scope="{ ds }">
              <v-my-data-table
                  :headers="headers"
                  :items="ds.filteredItems"
                  :loading="ds.isLoading"
                  :server-items-length="ds.totalItems"
                  :options.sync="ds.options"
              >
                <template v-slot:body.prepend="{ headers }">
                  <v-my-data-table-search-row :ds="ds" :headers="headers"/>
                </template>
                <template v-slot:item.actions="{ item }">
                  <v-list-item-details-action-button
                      :to="getDetailsUrl(item)"></v-list-item-details-action-button>
                </template>
                <template v-slot:item.name="{ item }">
                  <router-link :to="getDetailsUrl(item)">{{ item.name }}</router-link>
                </template>
                <template v-slot:item.createDate="{ item }">{{ $formatDateTime(item.createDate) }}</template>
                <template v-slot:no-data v-if="ds.isError">
                  <div v-if="ds.isError">{{ $t('common.errorMessage') }}</div>
                </template>
              </v-my-data-table>
            </template>
          </v-ajax-list-data-source>
        </v-card>
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <v-card>
          <v-section-toolbar caption="nugetPackages.applicationsHeader"></v-section-toolbar>
          <v-ajax-list-data-source :httpPath="httpPathApplications" :use-server-side-pagination="false">
            <template slot-scope="{ ds }">
              <v-my-data-table
                  :headers="applicationHeaders"
                  :items="ds.filteredItems"
                  :loading="ds.isLoading"
                  :server-items-length="ds.totalItems"
                  :options.sync="ds.options"
                  sort-by="applicationName"
              >
                <template v-slot:body.prepend="{ headers }">
                  <v-my-data-table-search-row :ds="ds" :headers="headers"/>
                </template>
                <template v-slot:item.actions="{ item }">
                  <v-list-item-details-action-button
                      :to="getDetailsUrl(item)"></v-list-item-details-action-button>
                </template>

                <template v-slot:item.createDate="{ item }">{{ $formatDateTime(item.createDate) }}</template>
                <template v-slot:item.applicationName="{ item }">
                  <router-link
                      :to="paths.getApplicationVersionDetails(item.applicationId,item.applicationVersionId)"
                      title="Details"
                      class="action-link"
                  >
                    {{ item.applicationName }}
                  </router-link>
                </template>
                <template v-slot:no-data v-if="ds.isError">
                  <div v-if="ds.isError">{{ $t('common.errorMessage') }}</div>
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
  props: {},
  data() {
    return {
      isLoading: true,
      isError: false,
      caption: this.$t("nugetPackageVersions.header") + ' ' + this.$route.params.id,
      goBackUrl: `/nuget-packages`,
      packageName: this.$route.params.id,
      httpPath: HttpClient.getNugetPackageVersionsPath(this.$route.params.id),
      httpPathApplications: HttpClient.getNugetPackageApplicationsPath(this.$route.params.id),
      headers: [
        {
          text: this.$t("common.fieldNames.actions"),
          value: "actions",
          sortable: false,
          class: "actions",
          filterable: false,
          width: 100,
        },
        {text: "Name", value: "name",},
        {text: "Version", value: "version",},
        {text: "Applications", value: "applicationVersionsCount",},
        {text: "Create date", value: "createDate",},
      ],
      applicationHeaders: [
        {text: "Application name", value: "applicationName", groupable: true,},
        {text: "Environment", value: "environmentId", groupable: true,},
        {text: "Application version", value: "applicationVersion", groupable: false,},
        {text: "PackageVersion", value: "packageVersion", groupable: true,},

      ],
    };
  },
  methods: {
    getDetailsUrl(item): string {
      return Paths.getNugetVersionPackageDetails(item.name, item.version);
    },

  },
  computed:{
    paths(): any {
      return Paths;
    },
  }
});
</script>
