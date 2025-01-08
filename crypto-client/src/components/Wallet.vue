<template>
  <div class="wallet-container">
    <h1>My Wallet</h1>
    <p>Username: {{ username }}</p>
    <p class="text-lg text-gray-700">Balance: <strong>$1000</strong></p>
    <p class="text-lg text-gray-700 mt-2">Coins: <strong>BTC, ETH</strong></p>
    <button @click="logout">Log out</button>
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
  width: 400px;
  margin: auto;
  padding: 30px;
  border-radius: 20px;
  background: white;
  box-shadow: 0 8px 16px rgba(0, 0, 0, 0.2);
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
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