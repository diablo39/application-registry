<template>
  <div>
    <div class="text-center" v-if="dialog">
      <v-dialog
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
    <v-container v-if="unauthorized">
      <v-card>
        <v-card-text class="headline font-weight-bold">
          <v-spacer></v-spacer>
          <h1 class="display-4" style="text-align: center">Unauthorized</h1>
          <v-spacer></v-spacer>
          <span style="display: block; text-align: center;">{{ errorDescription }}</span>
        </v-card-text>
      </v-card>
    </v-container>
  </div>
</template>


<script>
function getHashValue(key) {
  const matches = location.hash.match(new RegExp(key+'=([^&]*)'));
  return matches ? matches[1] : null;
}

export default {
  data() {
    return {
      dialog: true,
      unauthorized: false,
      errorDescription: ''
    }
  },
  async created() {
    try {
      const result = await this.$root.mgr.signinRedirectCallback();
      let returnToUrl = '/';
      if (result.state !== undefined) {
        returnToUrl = result.state;
      }
      this.dialog = false;
      await this.$router.push({path: returnToUrl});
    } catch (e) {
      this.errorDescription = decodeURI(getHashValue('error_description') || "" ).replaceAll('+', ' ');
      this.dialog = false;
      this.unauthorized = true;
    }
  }
}
</script>