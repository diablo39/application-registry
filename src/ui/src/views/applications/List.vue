<template>
  <v-container>
    <v-row>
      <v-col>
        <v-card>
          <v-section-toolbar :caption="caption">
            <template slot="endButtons">
              <router-link :to="getCreateUrl()" class="action-link">
                <v-btn color="success">
                  <v-icon>mdi-plus</v-icon>
                  Create
                </v-btn>
              </router-link>
            </template>
          </v-section-toolbar>
          <v-ajax-list-data-source :httpPath="httpPath">
            <template slot-scope="{ ds }">
              <v-my-data-table
                  :headers="headers"
                  :items="ds.filteredItems"
                  :loading="ds.isLoading"
                  :server-items-length="ds.totalItems"
                  :options.sync="ds.options"
                  :show-group-by="true"
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
                <template v-slot:item.name="{ item }">
                  <router-link :to="getDetailsUrl(item)">{{ item.name }}</router-link>
                </template>
                <template v-slot:item.createDate="{ item }">{{ $formatDateTime(item.createDate) }}</template>
                <template v-slot:item.projectName="{ item }">
                  <router-link :to="getProjectDetailsUrl(item)">{{ item.projectName }}</router-link>
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
      caption: "applications.header",
      httpPath: HttpClient.getApplicationsPath,
      headers: [
        {
          text: "Actions",
          value: "actions",
          sortable: false,
          class: "actions",
          filterable: false,
          groupable: false
        },
        {text: "Name", value: "name", groupable: false,},
        {text: "Code", value: "code", groupable: false,},
        {text: "Owner", value: "owner", groupable: false,},
        {text: "System", value: "projectName" , groupable: true},
        {text: "Framework", value: "framework" , groupable: true},
      ],
    };
  },
  methods:{
    getDetailsUrl(item): string {
      return Paths.getApplicationDetails(item.id);
    },
    getCreateUrl(): string {
      return Paths.createApplication();
    },
    getEditUrl(item): string {
      return Paths.editApplication(item.id, "list");
    },
    getProjectDetailsUrl(item): string {
      return Paths.getSystemDetails(item.projectId);
    },
  },
});
</script>
