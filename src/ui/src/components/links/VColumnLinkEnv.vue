<template>
  <router-link :to="getEnvDetailsUrl(env)">{{ env }}</router-link>
</template>

<script lang="ts">
import Vue from "vue";
import Paths from "@/router/Paths";

export default Vue.component('v-column-link-env',{
  props:{
    env:{
      type: String,
      required: true,
    },
  },
  methods: {
    getEnvDetailsUrl(env): string {
      return Paths.getEnvDetails(env);
    },
  },
});
</script>

<style scoped>

</style>