<template>
  <router-link :to="to" class="action-link">
    <v-btn color="success">
      <v-icon>{{icon}}</v-icon><span>Create</span>
    </v-btn>
  </router-link>
</template>

<script lang="ts">
import Vue from 'vue'
export default Vue.component('v-create-button', {
  props: {
    to:{
      type: String,
      required: true
    },
    icon:{
      default: "mdi-plus"
    }
  }
})
</script>

<style scoped>

</style>