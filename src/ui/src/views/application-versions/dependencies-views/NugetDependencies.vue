<template>
  <v-card flat>
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
          <template v-slot:item.createDate="{ item }">{{ $formatDateTime(item.createDate) }}</template>
          <template v-slot:no-data v-if="ds.isError">
            <div v-if="ds.isError">{{ $t('common.errorMessage') }}</div>
          </template>
        </v-my-data-table>
      </template>
    </v-ajax-list-data-source>
  </v-card>
</template>

<script lang="ts">
import Vue from "vue";

export default Vue.extend({
  data(){
    return {
      httpPath: `/api/ApplicationVersions/${this.$route.params.id}/dependencies/nugets`,
      headers: [
        {text: "Name", value: "name", groupable: false,},
        {text: "Version", value: "version", groupable: false,},
      ],
    }
  },
  mounted() {
  // mounted here
  }
});
</script>

<style scoped>

</style>