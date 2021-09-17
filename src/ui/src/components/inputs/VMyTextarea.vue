<template>
  <v-textarea
    v-model="value.value"
    :label="value.label"
    :rules="value.validationRules"
    :error-messages="value.errorMessages"
    @change="value.errorMessages = []"


  ></v-textarea>
</template>
<script lang="ts">
import Vue from "vue";
export default Vue.component("v-my-textarea", {
  props: {
    value: {},
  },
});
</script>