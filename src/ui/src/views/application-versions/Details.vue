<template>
  <v-container>
    <v-view-details-header :isError="isError" :isLoading="isLoading"></v-view-details-header>
    <v-view-details-toolbar v-if="dataLoaded" :caption="caption" :goBackUrl="goBackUrl">
      <template slot="endButtons">
        <router-link :to="`/applications/${item.applicationId}/application-versions/${item.id}/swagger`" class="action-link mr-4">
          <v-btn color="success" outlined>
            <img src="@/assets/swagger.png" alt="Swagger" style="width: 17px; vertical-align: middle; margin-right: 5px;"/>
            Swagger
          </v-btn>
        </router-link>
      </template>
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
          <v-section-toolbar caption="applicationVersionDependencies.dependenciesHeader"></v-section-toolbar>
          <v-tabs vertical>
            <v-tab class="text-left">
              <v-icon left>
                mdi-application
              </v-icon>
              Applications
            </v-tab>
            <v-tab class="text-left">
              <span>
                <img src="@/assets/NuGet-Logo.svg" alt="Nuget" style="width: 25px; vertical-align: middle"/>
                Nugets
              </span>
            </v-tab>

            <v-tab-item>
              <application-dependencies></application-dependencies>
            </v-tab-item>

            <v-tab-item>
              <nuget-dependencies></nuget-dependencies>
            </v-tab-item>

          </v-tabs>
        </v-card>
      </v-col>
    </v-row>

    <v-row>
      <v-col>
        <v-card>
          <v-section-toolbar caption="applicationVersionDependencies.endpointsHeader"></v-section-toolbar>
          <endpoints :application-version-id="id" :environment="item.environmentId"></endpoints>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import Vue from "vue";
import {HttpClient} from "@/services/httpClient/HttpClient";
import NugetDependencies from './dependencies-views/NugetDependencies.vue';
import ApplicationDependencies from "@/views/application-versions/dependencies-views/ApplicationDependencies.vue";
import Endpoints from "@/views/application-versions/_Endpoints.vue";
export default Vue.extend({
  data() {
    return {
      id: this.$route.params.id,
      isLoading: true,
      isError: false,
      item: {},
      captionTranslationKey: "applicationVersions.detailsHeader",
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
      return `/applications/${(this.item as any).applicationId}/details`
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
    goBack() {
      this.$router.push("/env");
    },
  },
  components: {
    NugetDependencies,
    ApplicationDependencies,
    Endpoints,
  },
});
</script>