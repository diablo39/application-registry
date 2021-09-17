<template>
  <div class="text-center">
    <v-dialog
        v-model="dialog"
        persistent
        width="300"
    >
      <v-card
          color="primary"
          dark
      >
        <v-card-text>
          Sign-in in progress
          <v-progress-linear
              indeterminate
              color="white"
              class="mb-0"
          ></v-progress-linear>
        </v-card-text>
      </v-card>
    </v-dialog>
  </div>
</template>



<script>
export default {
  data () {
    return {
      dialog: true,
    }
  },
  async created() {
    try {
      const result = await this.$root.mgr.signinRedirectCallback();
      let returnToUrl = '/';
      if (result.state !== undefined) { returnToUrl = result.state;}
      this.dialog = false;
      this.$router.push({ path: returnToUrl });
    } catch (e) {
      this.dialog = false;
      this.$router.push({ name: 'Unauthorized' });
    }
  }
}
</script>