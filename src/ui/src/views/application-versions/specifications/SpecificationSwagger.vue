<template>
  <v-container>
    <v-view-details-header :isError="false" :isLoading="false"></v-view-details-header>
    <v-view-details-toolbar :caption="caption" :dataLoaded="true" :goBackUrl="goBackUrl"></v-view-details-toolbar>
    <v-row>
      <v-col>
        <v-card>
          <v-card-text>
            <div id="swagger-ui"></div>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>
/* eslint-disable @typescript-eslint/camelcase */

import SwaggerUI from "swagger-ui";
import 'swagger-ui/dist/swagger-ui.css'
import VViewDetailsHeader from "@/components/VViewDetailsHeader.vue";
import VViewDetailsToolbar from "@/components/VViewDetailsToolbar.vue";

export default {
  data() {
    return {
      captionTranslationKey: "applications.detailsHeader",
      goBackUrl: `/applications/${ this.$route.params.applicationId}/application-versions/${this.$route.params.applicationVersionId}/details`,
      isLoading: true,
      isError: false,
      application: {},
      applicationVersions: [],
      applicationVersionsHeaders: [
        {text: "Actions", value: "actions", sortable: false, width: 100},
        {text: "Version", value: "version", groupable: true},
        {text: "Commit", value: "idCommit", groupable: true},
        {text: "Tools version", value: "toolsVersion", groupable: true},
        {text: "Create date", value: "createDate", groupable: true},
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
    const idApplicationVersion = this.$route.params.applicationVersionId;
    const id = this.$route.params.specificationId;
    // eslint-disable-next-line @typescript-eslint/no-this-alias
    const control = this;
    SwaggerUI({
      url: `${window.location.origin}/api/ApplicationVersions/${idApplicationVersion}/specifications/swaggers/${id}/text`,
      dom_id: "#swagger-ui",
      deepLinking: false,
      presets: [SwaggerUI.presets.apis],
      plugins: [],
      requestInterceptor: async function (request) {
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
<style>
.information-container > section {
  border: 1px solid lightblue;
  border-radius: 4px;
  box-shadow: 0 0 3px rgb(0 0 0 / 19%);
}
.info > hgroup > a {
  display: none;
}
</style>