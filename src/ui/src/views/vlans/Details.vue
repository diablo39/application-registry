<template>
  <v-container>
    <v-view-details-header :isError="isError" :isLoading="isLoading"></v-view-details-header>
    <v-view-details-toolbar v-if="dataLoaded" :caption="caption" :goBackUrl="goBackUrl">
      <template slot="endButtons">
        <router-link :to="'/vlans/' + id + '/edit'" class="action-link">
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
            <v-view-details-row label="CIDR" :value="item.cidr"></v-view-details-row>
            <v-view-details-row label="Name" :value="item.name"></v-view-details-row>
            <v-view-details-row label="Alias" :value="item.alias"></v-view-details-row>
            <v-view-details-row label="Number" :value="item.number"></v-view-details-row>
<!--            <v-view-details-row label="Environment" :value="item.env" :to="getEnvDetailsUrl(item)"></v-view-details-row>-->
            <v-view-details-row label="Description" :value="item.description"></v-view-details-row>
            <v-row col>
              <v-col cols="2">
                <p class="text-right">Is virtual directory:</p>
              </v-col>
              <v-col>
                <v-icon v-if="item.isVirtualDirectory">mdi-checkbox-marked</v-icon>
                <v-icon v-else>mdi-checkbox-blank-outline</v-icon>
              </v-col>
            </v-row>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
<!--    <v-row>-->
<!--      <v-col>-->
<!--        <v-card>-->
<!--          <v-section-toolbar caption="vlans.machinesHeader"></v-section-toolbar>-->
<!--          <v-in-memory-list-data-source :items="item.machines" :is-error="isError" :is-loading="isLoading">-->
<!--            <template slot-scope="{ ds }">-->
<!--              <v-my-data-table-->
<!--                  :headers="headers"-->
<!--                  :items="ds.filteredItems"-->
<!--                  :loading="ds.isLoading"-->
<!--                  :server-items-length="ds.totalItems"-->
<!--                  :options.sync="ds.options"-->
<!--                  sort-by="name"-->
<!--              >-->
<!--                <template v-slot:body.prepend="{ headers }">-->
<!--                  <v-my-data-table-search-row :ds="ds" :headers="headers"/>-->
<!--                </template>-->
<!--                <template v-slot:item.actions="{ item }">-->
<!--                  <v-list-item-details-action-button :to="`/machines/${item.fqdn}`"></v-list-item-details-action-button>-->
<!--                </template>-->
<!--                <template v-slot:item.name="{ item }"><router-link :to="`/machines/${item.fqdn}`">{{ item.name }}</router-link></template>-->
<!--                <template v-slot:item.createDate="{ item }">{{ $formatDateTime(item.createDate) }}</template>-->
<!--                <template v-slot:item.operatingSystem="{ item }">{{ item['operating-system-distribution'] }}-->
<!--                  {{ item['operating-system-version'] }}-->
<!--                </template>-->
<!--                <template v-slot:no-data v-if="isError">-->
<!--                  <div v-if="isError">{{ $t('common.errorMessage') }}</div>-->
<!--                </template>-->
<!--              </v-my-data-table>-->
<!--            </template>-->
<!--          </v-in-memory-list-data-source>-->
<!--        </v-card>-->
<!--      </v-col>-->
<!--    </v-row>-->
  </v-container>
</template>

<script lang="ts">
import Vue from "vue";
import {HttpClient} from "@/services/httpClient/HttpClient";
import Paths from "@/router/Paths";

export default Vue.extend({
  data() {
    return {
      goBackUrl: "/vlans",
      isLoading: true,
      isError: false,
      item: {},
      captionTranslationKey: "vlans.detailsHeader",
      id: this.$route.params.id,
      headers: [
        {
          text: this.$t("common.fieldNames.actions"),
          value: "actions",
          sortable: false,
          class: "actions",
          filterable: false,
          width: 100,
        },
        {text: "Name", value: "name", groupable: true,},
        {text: "FQDN", value: "fqdn", groupable: true,},
        {text: "Environment", value: "env", groupable: true,},
        {text: "Group", value: "group", groupable: true,},
        {text: "CPU", value: "vcpu", groupable: true,},
        {text: "Memory", value: "memory", groupable: true,},
        {text: "Operating system", value: "operatingSystem", groupable: true,},
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
  },
  methods: {
    loadData() {
      this.isLoading = true;
      const id: string = this.$route.params.id;
      HttpClient.getVlan(id)
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
    getEnvDetailsUrl(item): string {
      return Paths.getEnvDetails(item.env);
    },
  },
});
</script>