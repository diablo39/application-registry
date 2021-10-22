<template>
  <div></div>
</template>

<script>
export default {
  name: "SilentRenew",
  async created() {
    try {
      const result = await this.$root.mgr.signinSilentCallback();
      console.log(result);
    } catch (error) {
      console.log(error);
    }
  }
}
</script>

<style scoped>

</style>