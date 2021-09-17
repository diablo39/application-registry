<template>
    <router-link :to="to" class="action-link edit-action-button">
        <v-tooltip right>
            <template v-slot:activator="{ on, attrs }">
              <v-icon v-bind="attrs" v-on="on">mdi-pencil-box-outline</v-icon>
            </template>
            <span>{{$t('common.buttons.edit')}}</span>
        </v-tooltip>
    </router-link>
</template>

<script lang="ts">
import Vue from 'vue'
export default Vue.component('v-list-item-edit-action-button', {
    props: {
        to: {
            type: String,
            required: true
        }
    }
})
</script>
<style>
.edit-action-button, .edit-action-button i {
    color: #1E88E5 !important
}
</style>