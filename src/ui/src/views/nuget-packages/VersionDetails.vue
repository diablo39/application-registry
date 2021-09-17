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
            <v-view-details-row label="Name" :value="item.name"></v-view-details-row>
            <v-view-details-row label="Version" :value="item.version"></v-view-details-row>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <v-card>
          <v-section-toolbar caption="nugetPackageVersions.applicationsHeader"></v-section-toolbar>
          <v-in-memory-list-data-source :items="item.applications" :is-error="isError" :is-loading="isLoading">
            <template slot-scope="{ ds }">
              <v-my-data-table
                  :headers="headers"
                  :items="ds.filteredItems"
                  :loading="ds.isLoading"
                  :server-items-length="ds.totalItems"
                  :options.sync="ds.options"
                  sort-by="applicationName"
              >
                <template v-slot:body.prepend="{ headers }">
                  <v-my-data-table-search-row :ds="ds" :headers="headers"/>
                </template>
                <template v-slot:item.createDate="{ item }">{{ $formatDateTime(item.createDate) }}</template>
                <template v-slot:no-data v-if="isError">
                  <div v-if="isError">{{ $t('common.errorMessage') }}</div>
                </template>
                <template v-slot:item.applicationName="{ item }">
                  <router-link
                      :to="getApplicationDetailsUrl(item)"
                      title="Details"
                      class="action-link"
                  >
                    {{ item.applicationName }}
                  </router-link>
                </template>
                <template v-slot:item.applicationVersion="{ item }">
                  <router-link
                      :to="paths.getApplicationVersionDetails(item.applicationId,item.applicationVersionId)"
                      title="Details"
                      class="action-link"
                  >
                    {{ item.applicationVersion }}
                  </router-link>
                </template>
              </v-my-data-table>
            </template>
          </v-in-memory-list-data-source>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import Vue from "vue";
import {HttpClient} from "@/services/httpClient/HttpClient";
import Paths from "@/router/Paths";

export default Vue.extend({
  data() {
    return {
      goBackUrl: `/nuget-packages/${this.$route.params.name}/details`,
      isLoading: true,
      isError: false,
      item: {},
      captionTranslationKey: "nugetPackageVersions.detailsHeader",
      headers: [
        // {
        //   text: this.$t("common.fieldNames.actions"),
        //   value: "actions",
        //   sortable: false,
        //   class: "actions",
        //   filterable: false,
        //   width: 100,
        // },
        {text: "Application name", value: "applicationName", groupable: true,},
        {text: "Environment", value: "environmentId", groupable: true,},
        {text: "Version", value: "applicationVersion", groupable: true,},
      ],
    };
  },

  mounted() {
    this.loadData();
  },
  computed: {
    dataLoaded(): boolean {
      return !this.isError && !this.isLoading;
    },
    caption(): string {
      return this.$t(this.captionTranslationKey, this.item).toString();
    },
    paths(): any {
      return Paths;
    },
  },
  methods: {
    loadData() {
      this.isLoading = true;
      const packageName: string = this.$route.params.name;
      const packageVersion: string = this.$route.params.version;
      HttpClient.getNugetPackageDetails(packageName, packageVersion)
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
    getApplicationDetailsUrl(item): string {
      return Paths.getApplicationDetails(item.applicationId);
    },
  },
});
</script>