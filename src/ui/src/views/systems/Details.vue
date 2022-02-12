<template>
  <v-container>
    <v-view-details-header :isError="isError" :isLoading="isLoading"></v-view-details-header>
    <v-view-details-toolbar v-if="dataLoaded" :caption="caption" :goBackUrl="goBackUrl">
      <template slot="endButtons">
        <router-link :to="'/systems/' + id + '/edit'" class="action-link">
          <v-btn color="#1E88E5" dark>
            <v-icon>mdi-pencil</v-icon>Edit
          </v-btn>
        </router-link>
      </template>
    </v-view-details-toolbar>

    <v-row v-if="dataLoaded">
      <v-col>
        <v-card>
          <v-section-toolbar caption="common.sectionNames.general"></v-section-toolbar>
          <v-card-text>
            <v-view-details-row label="Name" :value="item.name"></v-view-details-row>
            <v-view-details-row label="Description" :value="item.description"></v-view-details-row>
            <v-view-details-row
              :label="$t('common.fieldNames.createDate')"
              :value="$formatDateTime(item.createDate)"
            ></v-view-details-row>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <v-card>
          <v-section-toolbar :caption="applicationSectionCaption">
            <template slot="endButtons">
              <router-link :to="getApplicationCreateUrl()" class="action-link">
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
                      :to="getApplicationDetailsUrl(item)"></v-list-item-details-action-button>
                  <v-list-item-edit-action-button
                      :to="getApplicationEditUrl(item)"></v-list-item-edit-action-button>
                </template>
                <template v-slot:item.name="{ item }">
                  <router-link :to="getApplicationDetailsUrl(item)">{{ item.name }}</router-link>
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
import { HttpClient } from "@/services/httpClient/HttpClient";
import Paths from "@/router/Paths";

export default Vue.extend({
  data() {
    return {
      goBackUrl: "/systems",
      isLoading: true,
      isError: false,
      item: {},
      captionTranslationKey: "systems.detailsHeader",
      applicationSectionCaptionTranslationKey: "applications.header",
      id:  this.$route.params.id,
      httpPath: HttpClient.getApplicationsPath + `?systemId=${this.$route.params.id}`,
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
        {text: "Framework", value: "framework" , groupable: true},
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
    applicationSectionCaption(): string {
      return this.$t(this.applicationSectionCaptionTranslationKey, this.item).toString();
    },
  },
  methods: {
    loadData() {
      this.isLoading = true;
      const id: string = this.$route.params.id;
      HttpClient.getSystem(id)
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
      this.$router.push("/systems");
    },
    getApplicationDetailsUrl(item): string {
      return Paths.getApplicationDetails(item.id);
    },
    getApplicationCreateUrl(): string {
      return Paths.createApplication();
    },
    getApplicationEditUrl(item): string {
      return Paths.editApplication(item.id, "list");
    },
  },
});
</script>