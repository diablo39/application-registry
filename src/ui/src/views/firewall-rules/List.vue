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
                  sort-by="source-host"
              >
                <template v-slot:body.prepend="{ headers }">
                  <v-my-data-table-search-row :ds="ds" :headers="headers"/>
                </template>
<!--                <template v-slot:item.actions="{ item }">-->
<!--                  <v-list-item-details-action-button-->
<!--                      :to="`/${detailsUrlSegment}/${item.id}`"></v-list-item-details-action-button>-->
<!--                </template>-->
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

export default Vue.extend({
  props: {},
  data() {
    return {
      caption: "firewallRules.header",
      detailsUrlSegment: "firewall-rules",
      httpPath: HttpClient.getFirewallRulesPath,
      headers: [
        // {
        //   text: this.$t("common.fieldNames.actions"),
        //   value: "actions",
        //   sortable: false,
        //   class: "actions",
        //   filterable: false,
        //   width: 100,
        // },
        {text: "Source host", value: "source-host", groupable: true,},
        {text: "Source ipv4", value: "source-ipv4", groupable: true,},
        {text: "Source env", value: "source-env", groupable: true,},
        {text: "Destination host", value: "destination-host", groupable: true,},
        {text: "Destination ipv4", value: "destination-ipv4", groupable: true,},
        {text: "Destination env", value: "destination-env", groupable: true,},
        {text: "Destination ports", value: "destination-ports",},

      ],
    };
  },
  methods: {
    //
  },

});
</script>
