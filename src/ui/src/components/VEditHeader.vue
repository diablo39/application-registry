<template>
  <v-row v-if="isLoading">
    <v-col>
      <v-card :loading="true">
        <v-card-text class="headline" style="text-align: center">{{ $t('common.loadingMessage') }}</v-card-text>
      </v-card>
    </v-col>
  </v-row>
  <v-row v-else-if="isError">
    <v-col>
      <v-card>
        <v-card-text class="headline" style="text-align: center">{{ $t('common.errorMessage') }}</v-card-text>
      </v-card>
    </v-col>
  </v-row>
</template>

<script lang="ts">
import Vue from 'vue'

export default Vue.component("v-edit-header", {
  props: {
    isLoading: {},
    isError: {},
  }
})
</script>