<template>
  <v-container>
    <v-view-details-header :isError="isError" :isLoading="isLoading"></v-view-details-header>
    <v-view-details-toolbar v-if="dataLoaded" :caption="caption"
                            :goBackUrl="goBackUrl"></v-view-details-toolbar>
    <v-row>
      <v-col>
        <v-card>
          <v-section-toolbar caption="endpointAnalysis.drillUpHeader">
            <template slot="startButtons">
              <v-icon class="success--text">mdi-arrow-top-left-thick</v-icon>
            </template>
          </v-section-toolbar>

          <v-ajax-list-data-source :httpPath="httpPath" :options="options" :use-server-side-pagination="false">
            <template slot-scope="{ ds }">
              <v-my-data-table
                  :headers="headers"
                  :items="ds.filteredItems"
                  :loading="ds.isLoading"
                  :server-items-length="ds.totalItems"
                  :options.sync="options"
                  sort-by="id"
              >
                <template v-slot:item.serviceIntended="{ item }">
                  <!--<span v-if="item.isLeaf" :style="{ paddingLeft: ((item.level -1 ) * 25) + 'px' }"><v-icon class="mdi-rotate-180">mdi-lastpass</v-icon>{{ item.applicationCode }}</span>
                  <span v-else :style="{ paddingLeft: ((item.level -1 ) * 25) + 'px' }"><v-icon>mdi-subdirectory-arrow-right</v-icon>{{ item.applicationCode }}</span>-->
                  <span :style="{ paddingLeft: ((item.level -1 ) * 25) + 'px' }"><v-icon>mdi-subdirectory-arrow-right</v-icon>{{ item.applicationCode }}</span>
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
import Vue from 'vue'
import Paths from '@/router/Paths';
import {HttpClient} from "@/services/httpClient/HttpClient";

export default Vue.extend({
  data() {
    return {
      id: this.$route.params.version,
      isLoading: false,
      isError: false,
      options: {},
      item: {},
      captionTranslationKey: "applicationVersions.detailsHeader",
      headers: [
        {text: "Service", value: "serviceIntended", groupable: false, sortable: false,},
        {text: "Http method", value: "httpMethod", groupable: false, sortable: false,},
        {text: "Path", value: "path", groupable: false, sortable: false,},
      ],
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
      const appVer = this.item as any;
      const httpMethod = this.$route.query.httpMethod.toString().toUpperCase();
      const path = this.$route.query.path;
      return `[${appVer.environmentId}] ${ appVer.applicationName } - ${httpMethod } ${path}`;
    },
    httpPath(): string {
      const version = this.$route.params.version;
      const httpMethod = this.$route.query.httpMethod;
      const path = this.$route.query.path;
      return HttpClient.getEndpointAnalysisDrillUp(httpMethod, path, version);
    },
    paths(): any {
      return Paths;
    },
    goBackUrl(): string {
      return Paths.getApplicationVersionDetails((this.item as any).applicationId, this.id);
      // `/applications/${application.id}/application-versions/${item.id}/details`
      // return Paths `/applications/${(this.item as any).applicationId}/details`
    },
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
  },
  components: {}
})
</script>

<style scoped>

</style>