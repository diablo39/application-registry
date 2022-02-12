<template>
  <v-row class="details-view-toolbar">
    <v-col>
      <v-card>
        <v-toolbar flat>
          <v-back-button :goBackUrl="goBackUrl"/>
          <v-toolbar-title class="headline">{{ caption }}</v-toolbar-title>
        </v-toolbar>
      </v-card>
    </v-col>
  </v-row>
</template>

<script lang="ts">
import Vue from "vue";

export default Vue.component("v-create-toolbar", {
  props: {
    goBackUrl: {
      type: String
    },
    caption: {
      type: String
    },
  },
  methods: {

  },
});
</script>