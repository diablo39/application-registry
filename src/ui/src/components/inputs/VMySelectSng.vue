<template>
  <v-select
      :items="items"
      :label="value.label"
      :item-text="value.itemText"
      :item-value="value.itemValue"
      v-model="value.value"
      :rules="value.validationRules"
      :error-messages="value.errorMessages"
      @change="value.errorMessages = []"


  ></v-select>
</template>
<script lang="ts">
import Vue from "vue";

export default Vue.component("v-my-select-sng", {
  props: {
    value: {},
    items: {
      default: [],
    }
  },
});
</script>