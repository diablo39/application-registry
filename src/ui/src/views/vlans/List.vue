<template>
  <v-container>
    <v-row>
      <v-col>
        <v-card>
          <v-section-toolbar :caption="caption">
            <template slot="endButtons">
              <v-create-button :to="getCreateUrl()"/>
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
                  sort-by="cidr"
                  :custom-sort="customSort"
              >
                <template v-slot:body.prepend="{ headers }">
                  <v-my-data-table-search-row :ds="ds" :headers="headers"/>
                </template>
                <template v-slot:item.actions="{ item }">
                  <v-list-item-details-action-button
                      :to="getDetailsUrl(item)"></v-list-item-details-action-button>
                  <v-list-item-edit-action-button
                      :to="getEditUrl(item)"></v-list-item-edit-action-button>
                  <v-icon style="margin-left: 10px" v-if="item.isVirtualDirectory"
                          title="Virtual entity - for organization only">mdi-folder-network
                  </v-icon>
                </template>
                <template v-slot:item.cidr="{ item }">
                  <router-link :to="getDetailsUrl(item)">{{ item.cidr }}</router-link>
                </template>
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
          width: 150,
        },
        {text: "CIDR", value: "cidr"},
        {text: "Name", value: "name",},
        {text: "Alias", value: "alias"},
        {text: "Number", value: "number",},
        // {text: "Environment", value: "env", groupable: true,},
        //
        // {text: "Machines count", value: "machines-count",},
        // {text: "Description", value: "description", groupable: false,},
      ],
    };
  },
  methods: {
    getDetailsUrl(item): string {
      return Paths.getVlanDetails(item.id);
    },
    getCreateUrl(): string {
      return Paths.createVlan();
    },
    getEditUrl(item): string {
      return Paths.editVlan(item.id, "list");
    },
    customSort(items, index, isDesc) {
      items.sort((a, b) => {
        if (index[0] === "cidr") {
          if (!isDesc[0]) {
            return a['cidrSortField'] < b['cidrSortField'] ? -1 : 1;
          } else {
            return b['cidrSortField'] < a['cidrSortField'] ? -1 : 1;
          }
        } else {
          if (!isDesc[0]) {
            return a[index] < b[index] ? -1 : 1;
          } else {
            return b[index] < a[index] ? -1 : 1;
          }
        }
      });
      return items;
    },
  },
});


</script>
<style type="text/css">
.is-virtual {
  background-color: #e9f1ff;
}
</style>