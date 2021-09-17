<script lang="ts">

import Vue from "vue";
import {VDataTable} from "vuetify/lib";

export default Vue.component('v-my-data-table',{
  extends: VDataTable,
  props: {
    "sort-by": {
      default: "name"
    },
    "items-per-page":{
      default: 15,
    },
    "footer-props": {
      default() {
        return {
          showFirstLastPage: true,
          firstIcon: 'mdi-arrow-collapse-left',
          lastIcon: 'mdi-arrow-collapse-right',
          prevIcon: 'mdi-minus',
          nextIcon: 'mdi-plus',
          itemsPerPageOptions: [5,10,15,25,50,-1],
        }
      }
    },
  },
})
</script>
