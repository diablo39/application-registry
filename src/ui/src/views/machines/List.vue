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
              >
                <template v-slot:body.prepend="{ headers }">
                  <v-my-data-table-search-row :ds="ds" :headers="headers"/>
                </template>
                <template v-slot:item.actions="{ item }">
                  <v-list-item-details-action-button
                      :to="getDetailsUrl(item)"></v-list-item-details-action-button>
                </template>
                <template v-slot:item.env="{ item }">
                  <router-link :to="getEnvDetailsUrl(item)">{{ item.env }}</router-link>
                </template>
                <template v-slot:item.name="{ item }">
                  <router-link :to="getDetailsUrl(item)">{{ item.name }}</router-link>
                </template>
                <template v-slot:item.createDate="{ item }">{{ $formatDateTime(item.createDate) }}</template>
                <template v-slot:no-data v-if="ds.isError">
                  <div v-if="ds.isError">{{ $t('common.errorMessage') }}</div>
                </template>
                <template v-slot:item.operatingSystem="{ item }">{{ item['operating-system-distribution'] }}
                  {{ item['operating-system-version'] }}
                </template>
                <template v-slot:item.vlans-list="{ item }">
                  <span class="vlanLink" v-for="vlan in item['vlans-list']" :key="vlan.cidr"><router-link :to="getVlanDetailsUrl(vlan)">{{ vlan.vlan }}</router-link></span>
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
      caption: "machines.header",
      detailsUrlSegment: "machines",
      httpPath: HttpClient.getMachinesPath,
      headers: [
        {
          text: this.$t("common.fieldNames.actions"),
          value: "actions",
          sortable: false,
          class: "actions",
          filterable: false,
          width: 100,
        },
        {text: "Name", value: "name",groupable: false,},
        {text: "FQDN", value: "fqdn",groupable: false,},
        {text: "Environment", value: "env", groupable: true,},
        {text: "Operating system", value: "operating-system", groupable: true,},
        {text: "CPU", value: "vcpu",groupable: false,},
        {text: "Memory", value: "memory",groupable: false,},
        {text: "Vlans", value: "vlans-list", groupable: false,},
        {text: "IP addresses", value: "ip", groupable: false,},
      ],
    };
  },
  methods: {
    getDetailsUrl(item): string {
      return Paths.getMachineDetails(item.id);
    },
    getEnvDetailsUrl(item): string {
      return Paths.getEnvDetails(item.env);
    },
    getVlanDetailsUrl(item): string {
      return Paths.getVlanDetails(item.cidr);
    },
  },
});
</script>
<style scoped>
.vlanLink:not(:first-child):before {
    content: ', ';
}
</style>