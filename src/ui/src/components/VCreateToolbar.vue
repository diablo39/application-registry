<template>
  <v-row class="details-view-toolbar">
    <v-col>
      <v-card>
        <v-toolbar flat>
          <v-btn v-if="goBackUrl" icon @click="goBack">
            <v-icon>mdi-arrow-left-bold</v-icon>
          </v-btn>
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
    goBack() {
      this.$router.push(this.goBackUrl);
    },
  },
});
</script>