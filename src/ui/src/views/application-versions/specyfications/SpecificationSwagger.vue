<template>
  <v-container>
    <v-view-details-header :isError="false" :isLoading="false"></v-view-details-header>
    <v-view-details-toolbar :caption="caption" :dataLoaded="true" :goBackUrl="goBackUrl"></v-view-details-toolbar>
    <div id="swagger-ui"></div>
  </v-container>
</template>
<script>
/* eslint-disable @typescript-eslint/camelcase */

import SwaggerUI from "swagger-ui";
import 'swagger-ui-themes/themes/3.x/theme-material.css'
import VViewDetailsHeader from "@/components/VViewDetailsHeader.vue";
import VViewDetailsToolbar from "@/components/VViewDetailsToolbar.vue";

export default {
  data() {
    return {
      captionTranslationKey: "applications.detailsHeader",
      goBackUrl: "/applications",
      isLoading: true,
      isError: false,
      application: {},
      applicationVersions: [],
      applicationVersionsHeaders: [
        { text: "Actions", value: "actions", sortable: false, width: 100 },
        { text: "Version", value: "version", groupable: true },
        { text: "Commit", value: "idCommit", groupable: true },
        { text: "Tools version", value: "toolsVersion", groupable: true },
        { text: "Create date", value: "createDate", groupable: true },
      ],
    };
  },
  computed: {
    dataLoaded() {
      return !this.isError && !this.isLoading;
    },
    caption() {
      return this.$t(this.captionTranslationKey, this.application);
    },
  },
  mounted() {
    const id = this.$route.params.applicationVersionId;
    // eslint-disable-next-line @typescript-eslint/no-this-alias
    const control = this;
    SwaggerUI({
      url: `${window.location.origin}/api/ApplicationVersions/${id}/specifications/swagger`,
      dom_id: "#swagger-ui",
      deepLinking: false,
      presets: [SwaggerUI.presets.apis],
      plugins: [SwaggerUI.plugins.DownloadUrl],
      requestInterceptor: async function(request){
        const token = await control.$root.mgr.getAccessToken();
        request.headers.Authorization = `Bearer ${token}`;
        return request;
      }
    });
  },
  components: {
    "v-view-details-header": VViewDetailsHeader,
    "v-view-details-toolbar": VViewDetailsToolbar,
  },
};
</script>