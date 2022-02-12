<template>
  <v-row class="details-view-toolbar">
    <v-col>
      <v-card>
        <v-toolbar flat>
          <v-back-button :goBackUrl="goBackUrl"/>
          <v-toolbar-title class="headline">{{ caption }}</v-toolbar-title>
          <v-spacer></v-spacer>
          <slot name="endButtons"></slot>

        </v-toolbar>
      </v-card>
    </v-col>
  </v-row>
</template>

<script lang="ts">
import Vue from "vue";

export default Vue.component('v-view-details-toolbar', {
  props: {
    goBackUrl: {
      type: String,
    },
    caption: {
      type: String,
    },
  },
  methods: {
    goBack() {
      this.$router.push(this.goBackUrl);
    },
  },
});
</script>