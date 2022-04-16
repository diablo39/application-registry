<template>
  <v-navigation-drawer app src="@/assets/stars-background.jpg" dark>
    <!-- v-model="drawerComputed" -->
    <v-list-item>
      <v-list-item-content>
        <v-list-item-title class="title">
          Application Registry
        </v-list-item-title>
      </v-list-item-content>
    </v-list-item>

    <v-divider></v-divider>

    <template v-slot:img="props">
      <v-img
          :gradient="`to bottom, rgba(0, 0, 0, 0.7), rgba(0, 0, 0, 0.7)`"
          v-bind="props"
      />
    </template>
    <v-list dense rounded v-if="userCanViewMenu">
      <v-list-item link to="/">
        <v-list-item-icon>
          <v-icon>mdi-home</v-icon>
        </v-list-item-icon>
        <v-list-item-title class="">Home</v-list-item-title>
      </v-list-item>
      <!-- <v-subheader class="mt-4">Global</v-subheader> -->
      <v-list-item link to="/env" >
        <v-list-item-icon>
          <v-icon>mdi-sort</v-icon>
        </v-list-item-icon>
        <v-list-item-title>Environments</v-list-item-title>
      </v-list-item>
      <!-- Network-->
      <v-subheader class="mt-4">Network</v-subheader>
      <v-list-item link to="/vlans">
        <v-list-item-icon>
          <v-icon>mdi-lan</v-icon>
        </v-list-item-icon>
        <v-list-item-title>VLAN</v-list-item-title>
      </v-list-item>
<!--      <v-list-item link to="/load-balancers">-->
<!--        <v-list-item-icon>-->
<!--          <v-icon>mdi-sitemap</v-icon>-->
<!--        </v-list-item-icon>-->
<!--        <v-list-item-title>Load balancers</v-list-item-title>-->
<!--      </v-list-item>-->
<!--      <v-list-item link to="/firewall-rules">-->
<!--        <v-list-item-icon>-->
<!--          <v-icon>mdi-security-network</v-icon>-->
<!--        </v-list-item-icon>-->
<!--        <v-list-item-title>Firewall rules</v-list-item-title>-->
<!--      </v-list-item>-->
      <!-- Infrastructure -->
<!--      <v-subheader class="mt-4">Infrastructure</v-subheader>-->
<!--      <v-list-item link to="/machines">-->
<!--        <v-list-item-icon>-->
<!--          <v-icon>mdi-server</v-icon>-->
<!--        </v-list-item-icon>-->
<!--        <v-list-item-title>Machines</v-list-item-title>-->
<!--      </v-list-item>-->
      <!-- Software -->
      <v-subheader class="mt-4">Software</v-subheader>
      <v-list-item link to="/systems">
        <v-list-item-icon>
          <v-icon>mdi-format-list-bulleted-square</v-icon>
        </v-list-item-icon>
        <v-list-item-title>Systems</v-list-item-title>
      </v-list-item>
      <v-list-item link to="/applications">
        <v-list-item-icon>
          <v-icon>mdi-application</v-icon>
        </v-list-item-icon>
        <v-list-item-title>Applications</v-list-item-title>
      </v-list-item>
<!--      <v-list-item link to="/redis">-->
<!--        <v-list-item-icon>-->
<!--          <v-icon>mdi-alpha-r</v-icon>-->
<!--        </v-list-item-icon>-->
<!--        <v-list-item-title>Redis</v-list-item-title>-->
<!--      </v-list-item>-->

      <v-list-group prepend-icon="mdi-package">
        <template v-slot:activator>
          <v-list-item-title>Packages</v-list-item-title>
        </template>

        <v-list-item link to="/nuget-packages">
          <v-list-item-icon>
            <img src="@/assets/NuGet-Logo.svg" alt="Nuget" style="width: 25px; vertical-align: middle" />
          </v-list-item-icon>
          <v-list-item-title>Nugets</v-list-item-title>

        </v-list-item>

      </v-list-group>
    </v-list>
  </v-navigation-drawer>
</template>

<script lang="ts">
import Vue from "vue";

export default Vue.component('v-side-menu', {
  name: "v-side-menu",
  props: ["drawer"],
  data: () => ({}),
  computed: {
    drawerComputed: function () {
      return this.drawer;
    },
    userCanViewMenu: function() {
      const app = (this.$root as any).$data || { isAuthenticated: false};
      if (app.isAuthenticated) {
        //already signed in, we can navigate anywhere
        return true;
      }
      return false;
    }
  },
});
</script>