<template>
  <div class="wallet-container">
    <h1>My Wallet</h1>
    <p>Username: {{ username }}</p>
    <p class="text-lg text-gray-700">Balance: <strong>$1000</strong></p>
    <p class="text-lg text-gray-700 mt-2">Coins: <strong>BTC, ETH</strong></p>
    <button @click="handleLogout">Log out</button>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { useAuthStore } from "@/stores/Auth";

export default defineComponent({
  name: "Wallet",
  data() {
    const authStore = useAuthStore();
    return {
      username: authStore.user?.username || "Guest", // Initialize username from the store
    };
  },
  methods: {
    handleLogout() {
      const authStore = useAuthStore(); // Access the Auth store
      authStore.logout(); // Log out the user
      this.$router.push("/"); // Redirect to the login page
    },
  },
});
</script>

<style>
.wallet-container {
  width: 100%;
  max-width: 400px;
  padding: 40px;
  border-radius: 20px;
  background-color: white;
  box-shadow: 0px 12px 24px rgba(0, 0, 0, 0.3);
}
button {
  width: 100%;
  padding: 10px;
  background-color: #000000;
  color: white;
  border: none;
  border-radius: 10px;
  font-size: 20px;
  cursor: pointer;
  margin-top: 15px;
}

button:hover {
  background-color: #41454b;
}
</style>