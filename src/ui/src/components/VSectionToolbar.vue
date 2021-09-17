<template>
  <v-toolbar flat>
    <slot name="startButtons"></slot>
    <v-toolbar-title>{{ translatedCaption }}</v-toolbar-title>
    <v-spacer></v-spacer>
    <slot name="endButtons"></slot>
  </v-toolbar>
</template>

<script lang="ts">
import Vue from "vue";
import VueI18n from "vue-i18n";

export default Vue.component('v-section-toolbar',{
  props: {
    caption: {
      required: true,
      type: String
    },
  },

  computed: {
    translatedCaption(): VueI18n.TranslateResult {
      return this.$t(this.caption);
    }
  },

});
</script>

<style scoped>

</style>