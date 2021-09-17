<template>
    <router-link :to="to" class="action-link details-action-button">
        <v-tooltip right>
            <template v-slot:activator="{ on, attrs }">
              <v-icon class="success--text" v-bind="attrs" v-on="on">{{icon}}</v-icon>
            </template>
            <span>{{$t('common.buttons.details')}}</span>
        </v-tooltip>
    </router-link>
</template>

<script lang="ts">
import Vue from 'vue'
export default Vue.component('v-list-item-details-action-button', {
    props: {
        to:{
            type: String,
            required: true
        },
      icon:{
          default: "mdi-eye"
      }
    }
})
</script>
<style>
.details-action-button  {
 margin-right: 10px;
}
</style>