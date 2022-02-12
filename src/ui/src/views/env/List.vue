<template>
  <v-container>
    <v-row>
      <v-col>
        <v-card>
          <v-section-toolbar :caption="caption">
            <template slot="endButtons">
              <v-create-button :to="getCreateUrl()"></v-create-button>
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
                <template v-slot:body.prepend="{ headers }">
                  <v-my-data-table-search-row :ds="ds" :headers="headers"/>
                </template>
                <template v-slot:item.actions="{ item }">
                  <v-list-item-details-action-button
                      :to="getDetailsUrl(item)"></v-list-item-details-action-button>
                  <v-list-item-edit-action-button
                      :to="getEditUrl(item)"></v-list-item-edit-action-button>
                </template>
                <template v-slot:item.id="{ item }">
                  <router-link :to="getDetailsUrl(item)">{{ item.id }}</router-link>
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
  </v-container>
</template>

<script lang="ts">
import Vue from "vue";
import {HttpClient} from "@/services/httpClient/HttpClient";
import Paths from "@/router/Paths";

export default Vue.extend({
  props: {},
  data() {
    return {
      caption: "env.header",
      httpPath: HttpClient.getEnvironmentsPath,
      options: {},
      headers: [
        {
          text: this.$t("common.fieldNames.actions"),
          value: "actions",
          sortable: false,
          class: "actions",
          filterable: false,
          width: 100,
        },
        {text: "Id", value: "id",},
        {text: "Description", value: "description",},
        {
          text: this.$t("common.fieldNames.createDate"),
          value: "createDate",
          filterable: false,
        },
      ],
    };
  },
  methods: {
    getDetailsUrl(item): string {
      return Paths.getEnvDetails(item.id);
    },
    getCreateUrl(): string {
      return Paths.createEnv();
    },
    getEditUrl(item): string {
      return Paths.editEnv(item.id, "list");
    },
  },

});
</script>
