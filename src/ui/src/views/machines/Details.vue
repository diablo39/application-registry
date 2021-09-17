<template>
  <v-container>
    <v-view-details-header :isError="isError" :isLoading="isLoading"></v-view-details-header>
    <v-view-details-toolbar v-if="dataLoaded" :caption="caption" :goBackUrl="goBackUrl">
    </v-view-details-toolbar>

    <v-row v-if="dataLoaded">
      <v-col>
        <v-card>
          <v-section-toolbar caption="common.sectionNames.general"></v-section-toolbar>
          <v-card-text>
            <v-view-details-row label="Name" :value="item.name"></v-view-details-row>
            <v-view-details-row label="FQDN" :value="item.fqdn"></v-view-details-row>
            <v-view-details-row label="Environment" :value="item.env" :to="getEnvDetailsUrl(item)"></v-view-details-row>
            <v-view-details-row label="Group" :value="item.group"></v-view-details-row>
            <v-view-details-row label="Operating system type"
                                :value="item['operating-system-class']"></v-view-details-row>
            <v-view-details-row label="Operating system" :value="item['operating-system']"></v-view-details-row>
            <v-view-details-row label="CPU" :value="item.vcpu"></v-view-details-row>
            <v-view-details-row label="Memory" :value="item.memory"></v-view-details-row>
            <v-view-details-row label="Description" :value="item.description"></v-view-details-row>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <v-card>
          <v-section-toolbar caption="machines.networkInterfacesHeader"></v-section-toolbar>
            <v-in-memory-list-data-source :items="item['network-interfaces']" :is-error="isError" :is-loading="isLoading">
              <template slot-scope="{ ds }">
                <v-my-data-table
                    :headers="networkInterfacesHeaders"
                    :items="ds.filteredItems"
                    :loading="ds.isLoading"
                    :server-items-length="ds.totalItems"
                    :options.sync="ds.options"
                    sort-by="name"
                >
                  <template v-slot:body.prepend="{ headers }">
                    <v-my-data-table-search-row :ds="ds" :headers="headers"/>
                  </template>
                  <template v-slot:item.createDate="{ item }">{{ $formatDateTime(item.createDate) }}</template>
                  <template v-slot:item.ipv4-cidr="{ item }">
                    <router-link :to="getVlanDetailsUrl(item)">{{ item['ipv4-cidr'] }}</router-link>
                  </template>
                  <template v-slot:no-data v-if="isError">
                    <div v-if="isError">{{ $t('common.errorMessage') }}</div>
                  </template>
              </v-my-data-table>
            </template>
          </v-in-memory-list-data-source>
        </v-card>
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <v-card>
          <v-section-toolbar caption="machines.dataVolumesHeader"></v-section-toolbar>
            <v-in-memory-list-data-source :items="item['data-volumes']" :is-error="isError" :is-loading="isLoading">
              <template slot-scope="{ ds }">
                <v-my-data-table
                    :headers="dataVolumesHeaders"
                    :items="ds.filteredItems"
                    :loading="ds.isLoading"
                    :server-items-length="ds.totalItems"
                    :options.sync="ds.options"
                    sort-by="mount"
                >
                  <template v-slot:body.prepend="{ headers }">
                    <v-my-data-table-search-row :ds="ds" :headers="headers"/>
                  </template>
                  <template v-slot:item.createDate="{ item }">{{ $formatDateTime(item.createDate) }}</template>
                  <template v-slot:no-data v-if="isError">
                    <div v-if="isError">{{ $t('common.errorMessage') }}</div>
                  </template>
                </v-my-data-table>
            </template>
          </v-in-memory-list-data-source>
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
  data() {
    return {
      goBackUrl: "/machines",
      isLoading: true,
      isError: false,
      item: {},
      captionTranslationKey: "machines.detailsHeader",
      id: this.$route.params.id,
      networkInterfacesHeaders: [
        {text: "Name", value: "name", groupable: true,},
        {text: "IP Address", value: "ipv4-address", groupable: true,},
        {text: "VLAN", value: "ipv4-vlan", groupable: true,},
        {text: "CIDR", value: "ipv4-cidr", groupable: true,},
        {text: "Netmask", value: "ipv4-netmask", groupable: true,},
        {text: "Network", value: "ipv4-network", groupable: true,},

      ],
      dataVolumesHeaders: [
        {text: "Device", value: "device", groupable: true,},
        {text: "Mount point", value: "mount", groupable: true,},
        {text: "Size [GB]", value: "size", groupable: true,},
        {text: "fstype", value: "fstype", groupable: true,},

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
      HttpClient.getMachine(id)
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
      this.$router.push("/machines");
    },
    getEnvDetailsUrl(item): string {
      return Paths.getEnvDetails(item.env);
    },
    getVlanDetailsUrl(item): string {
      return Paths.getVlanDetails(item['ipv4-cidr'].replace("/","_"));
    },
  },
});
</script>