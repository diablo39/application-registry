<template>
  <v-text-field
    v-model="value.value"
    :rules="value.validationRules"
    :label="value.label"
    :error-messages="value.errorMessages"
    :readonly = "value.readonly"
    @change="value.errorMessages = []"
    single-line
    type="number"
    
  ></v-text-field>
</template>
<script lang="ts">
import Vue from "vue";
export default Vue.component("v-my-number-field", {
  props: {
    value: {},
    readonly:{
      default: false
    }
  },
});
</script>