import Vue from "vue";


const DateTimeMixin = Vue.mixin({
    methods: {
      formatDateTime(d: string): string {
        return new Date(d).toLocaleString();
      },
    },
  });

export default  DateTimeMixin;