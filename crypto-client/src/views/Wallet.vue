<template>
  <div class="wallet-container">
    <h2>Wallet</h2>
    <p>Your crypto wallet details will appear here.</p>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "Wallet",
});
</script>

<style scoped>
.wallet-container {
  text-align: center;
  margin: 2rem;
}
</style>