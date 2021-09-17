<template>
  <v-container>
    <v-view-details-header :isError="isError" :isLoading="isLoading"></v-view-details-header>
    <v-view-details-toolbar v-if="dataLoaded" :caption="caption" :goBackUrl="goBackUrl">
      <template slot="endButtons">
        <router-link :to="'/env/' + id + '/edit'" class="action-link">
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
            <v-view-details-row label="Id" :value="item.id"></v-view-details-row>
            <v-view-details-row label="Description" :value="item.description"></v-view-details-row>
            <v-view-details-row
              :label="$t('common.fieldNames.createDate')"
              :value="$formatDateTime(item.createDate)"
            ></v-view-details-row>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import Vue from "vue";
import { HttpClient } from "@/services/httpClient/HttpClient";

export default Vue.extend({
  name: "EnvironmentDetails",
  data() {
    return {
      goBackUrl: "/env",
      isLoading: true,
      isError: false,
      item: {},
      captionTranslationKey: "env.detailsHeader",
      id:  this.$route.params.id,
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
  },
  methods: {
    loadData() {
      this.isLoading = true;
      const id: string = this.$route.params.id;
      HttpClient.getEnvironment(id)
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
});
</script>