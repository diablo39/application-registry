<template>
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
        <template v-slot:body.prepend="{ headers }">
          <v-my-data-table-search-row :ds="ds" :headers="headers"/>
        </template>
        <template v-slot:item.actions="{ item }">
          <router-link :to="`/endpoint-analysis/drill-up/${applicationVersionId}?httpMethod=${item.httpMethod}&environment=${environment}&path=${item.path}`" class="action-link">
            <v-tooltip right>
              <template v-slot:activator="{ on, attrs }">
                <v-icon class="success&#45;&#45;text" style="color: blue !important" v-bind="attrs" v-on="on">
                  mdi-arrow-top-left-thick
                </v-icon>
              </template>
              <span>Drill up</span>
            </v-tooltip>
          </router-link>
          <router-link :to="`/endpoint-analysis/drill-down/${applicationVersionId}?httpMethod=${item.httpMethod}&environment=${environment}&path=${item.path}`" class="action-link">
            <v-tooltip right>
              <template v-slot:activator="{ on, attrs }">
                <v-icon class="success--text" v-bind="attrs" v-on="on">mdi-arrow-bottom-right-thick</v-icon>
              </template>
              <span>Drill down</span>
            </v-tooltip>
          </router-link>

        </template>
      </v-my-data-table>
    </template>
  </v-ajax-list-data-source>
</template>

<script lang="ts">
import Vue from "vue";
import Paths from "@/router/Paths";

export default Vue.extend({
  props: {
    applicationVersionId: {
      required: true
    },
    environment: {
      required: true
    }
  },
  data() {
    return {
      httpPath: Paths.getApplicationEndpoints(this.applicationVersionId),
      captionTranslationKey: "applicationVersions.detailsHeader",
      options: {},
      headers: [
        {
          text: this.$t("common.fieldNames.actions"),
          value: "actions",
          sortable: false,
          class: "actions",
          filterable: false,
          width: 100
        },
        {text: "Name", value: "operationId", groupable: false,},
        {text: "Http method", value: "httpMethod", groupable: false,},
        {text: "Path", value: "path", groupable: false,},
      ]
    };
  },

  async mounted() {
//    await this.loadData();
  },

});
</script>

<style scoped>

</style>