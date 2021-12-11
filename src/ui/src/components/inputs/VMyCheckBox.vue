<template>
  <v-checkbox
      v-model="value.value"
      :label="value.label"
      :readonly = "value.readonly"
  ></v-checkbox>
</template>
<script lang="ts">
import Vue from "vue";
export default Vue.component("v-my-checkbox-field", {
  props: {
    value: {},
    readonly:{
      default: false
    }
  },
});
</script>