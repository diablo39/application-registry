<template>
  <tr v-if="!ds.isLoading && !ds.isError">
    <td v-for="header in headers" :key="header.value">
      <v-text-field
          v-if="header.filterable === true || header.filterable === undefined"
          v-model="ds.filters[header.value]"
          type="text"
          dense

          hide-details="auto"
          append-icon="mdi-magnify"
      ></v-text-field>
    </td>
  </tr>
</template>
<script lang="ts">
import Vue from "vue";

export default Vue.component('v-my-data-table-search-row',{
  name: '',
  props: {
    ds: {},
    headers: {}
  }
})
</script>