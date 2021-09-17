<template>
  <v-container>
    <v-row>
      <v-col>
        <v-card>
          <v-section-toolbar :caption="caption">
          </v-section-toolbar>
          <v-ajax-list-data-source :httpPath="httpPath">
            <template slot-scope="{ ds }">
              <v-my-data-table
                  :headers="headers"
                  :items="ds.filteredItems"
                  :loading="ds.isLoading"
                  :server-items-length="ds.totalItems"
                  :options.sync="ds.options"
                  sort-by="cidr"
              >
                <template v-slot:body.prepend="{ headers }">
                  <v-my-data-table-search-row :ds="ds" :headers="headers"/>
                </template>
                <template v-slot:item.actions="{ item }">
                  <v-list-item-details-action-button
                      :to="getDetailsUrl(item)"></v-list-item-details-action-button>
                </template>
                <template v-slot:item.cidr="{ item }"><router-link :to="getDetailsUrl(item)">{{ item.cidr }}</router-link></template>
                <template v-slot:item.createDate="{ item }">{{ $formatDateTime(item.createDate) }}</template>
                <template v-slot:item.env="{ item }">
                  <v-column-link-env :env="item.env"></v-column-link-env>
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
      caption: "vlans.header",
      httpPath: HttpClient.getVlansPath,
      headers: [
        {
          text: this.$t("common.fieldNames.actions"),
          value: "actions",
          sortable: false,
          class: "actions",
          filterable: false,
          width: 100,
        },
        {text: "CIDR", value: "cidr",},
        {text: "Vlan", value: "vlan",},
        {text: "Name", value: "name",},
        {text: "Environment", value: "env", groupable: true,},
        
        {text: "Machines count", value: "machines-count",},
        {text: "Description", value: "description", groupable: true,},
      ],
    };
  },
  methods: {
    getDetailsUrl(item): string {
      return Paths.getVlanDetails(item.id);
    },
  },
});
</script>
