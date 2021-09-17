<template>
  <v-text-field
    v-model="value.value"
    :rules="value.validationRules"
    :label="value.label"
    :error-messages="value.errorMessages"
    :readonly = "value.readonly"
    @change="value.errorMessages = []"

    
  ></v-text-field>
</template>
<script lang="ts">
import Vue from "vue";
export default Vue.component("v-my-text-field", {
  props: {
    value: {},
    readonly:{
      default: false
    }
  },
});
</script>