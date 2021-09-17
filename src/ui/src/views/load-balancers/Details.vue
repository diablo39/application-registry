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
            <v-view-details-row label="IP" :value="item['ip-v4']"></v-view-details-row>
            <v-view-details-row label="Port" :value="item.port"></v-view-details-row>
            <v-view-details-row label="Description" :value="item.description"></v-view-details-row>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <v-card>
          <v-section-toolbar caption="loadBalancers.membersHeader"></v-section-toolbar>
          <v-in-memory-list-data-source :items="memberPoolsFlatten">
            <template slot-scope="{ ds }">
              <v-my-data-table
                  :headers="membersHeaders"
                  :items="ds.filteredItems"
                  :loading="ds.isLoading"
                  :server-items-length="ds.totalItems"
                  :options.sync="ds.options"
                  group-by="name"
              >
                <template v-slot:body.prepend="{ headers }">
                  <v-my-data-table-search-row :ds="ds" :headers="headers"/>
                </template>
                <template v-slot:item.createDate="{ item }">{{ $formatDateTime(item.createDate) }}</template>
                <template v-slot:item.operatingSystem="{ item }">{{ item['operating-system-distribution'] }}
                  {{ item['operating-system-version'] }}
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
  </v-container>
</template>

<script lang="ts">
import Vue from "vue";
import {HttpClient} from "@/services/httpClient/HttpClient";

export default Vue.extend({
  data() {
    return {
      goBackUrl: "/load-balancers",
      isLoading: true,
      isError: false,
      item: {},
      captionTranslationKey: "loadBalancers.detailsHeader",
      id: this.$route.params.id,
      membersHeaders: [
        {text: "Pool name", value: "name", groupable: true,},
        {text: "Member Name", value: "memberName", groupable: true,},
        {text: "Group description", value: "description", groupable: true,},
        {text: "Ip v4", value: "memberIpV4", groupable: true,},
        {text: "Port", value: "port", groupable: true,},
        {text: "Balancing", value: "balancing", groupable: true,},
        {text: "Monitor", value: "monitor", groupable: true,},
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
    operatingSystem(): string {
      return this.item['operating-system-distribution'] + " " + this.item['operating-system-version'];
    },

    memberPoolsFlatten(): any {

      const result = new Array<any>();

      if (!this.item['member-pools'])
        return result;

      for (const memberPool of (this.item['member-pools'] as any)) {

        for (const member of (memberPool["members"] as any)) {
          const resultItem = {
            name: memberPool["name"],
            description: memberPool["description"],
            port: memberPool["port"],
            balancing: memberPool["balancing"],
            monitor: memberPool["monitor"],
            memberName: member["member-name"],
            memberIpV4: member["host"]
          };

          result.push(resultItem);
        }

      }

      return result;
    }
  },
  methods: {
    loadData() {
      this.isLoading = true;
      const id: string = this.$route.params.id;
      HttpClient.getLoadBalancer(id)
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
  },
});
</script>