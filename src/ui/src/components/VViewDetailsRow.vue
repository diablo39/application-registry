<template>
  <v-row col>
    <v-col cols="2">
      <p class="text-right">{{ label }}:</p>
    </v-col>
    <v-col>
      <router-link :to="to" v-if="to">{{ value }}</router-link>
      <span class="view-detail-row-text" style="white-space: pre;" v-else>{{ value }}</span>
    </v-col>
  </v-row>
</template>

<script lang="ts">
import Vue from "vue";

export default Vue.component('v-view-details-row', {
  name: "VViewDetailsRow",
  props: {
    label: {},
    value: {},
    to: {
      required: false
    },
  },
});
</script>