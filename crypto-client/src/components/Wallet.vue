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
import { defineComponent, computed } from "vue";
import { useAuthStore } from "@/stores/Auth";
import { useRouter } from "vue-router";

export default defineComponent({
  name: "Wallet",
  setup() {
    // Access the Auth store and Vue Router
    const authStore = useAuthStore();
    const router = useRouter();

    // Computed property for username
    const username = computed(() => authStore.user?.username || "Guest");

    // Logout method
    const handleLogout = () => {
      authStore.logout(); // Log out the user
      router.push("/"); // Redirect to the login page
    };

    // Return bindings for the template
    return {
      username,
      handleLogout,
    };
  },
});
</script>

<style scoped>
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