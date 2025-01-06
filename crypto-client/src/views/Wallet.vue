<template>
  <div class="wallet-container">
    <h1>My Wallet</h1>
    <p>Username: {{ username }}</p>
    <p class="text-lg text-gray-700">Balance: <strong>$1000</strong></p>
    <p class="text-lg text-gray-700 mt-2">Coins: <strong>BTC, ETH</strong></p>
    <button @click="logout">Logout</button>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { useRouter } from "vue-router";

export default defineComponent({
  setup() {
    const router = useRouter();
    const username = localStorage.getItem("username") || "Guest";

    const logout = () => {
      localStorage.removeItem("token");
      router.push("/");
    };

    return { username, logout };
  },
});
</script>

<style>
.wallet-container {
  max-width: 400px;
  margin: auto;
  text-align: center;
  padding: 20px;
  border: 1px solid #ccc;
  border-radius: 8px;
  background-color: #f9f9f9;
}
</style>