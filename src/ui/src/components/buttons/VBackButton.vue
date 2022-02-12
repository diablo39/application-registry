<template>
  <v-btn v-if="goBackUrl" icon @click="this.goBack">
    <v-icon>mdi-arrow-left-bold</v-icon>
  </v-btn>
</template>
<script lang="ts">
import Vue from "vue";

export default Vue.component('v-back-toolbar', {
  name: 'v-back-button',
  props: {
    goBackUrl:{
      type: String,
      required: true
    },
  },
  methods: {
    goBack() {
      this.$router.push(this.goBackUrl);
    },
  },
})
</script>