<template>
  <div class="signup-container">
    <h2>Sign Up</h2>
    <form @submit.prevent="signUp">
      <label for="username">Username:</label>
      <input id="username" v-model="username" type="text" />
      
      <label for="password">Password:</label>
      <input id="password" v-model="password" type="password" />

      <button type="submit">Sign Up</button>
    </form>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "SignUp",
  data() {
    return {
      username: "",
      password: "",
    };
  },
  methods: {
    async signUp() {
      try {
        const response = await fetch("http://localhost:5040/api/Auth", {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({ username: this.username, password: this.password }),
        });
        if (!response.ok) throw new Error("Sign Up failed");
        const data = await response.json();
        console.log("Sign Up successful:", data);
      } catch (error) {
        console.error(error);
      }
    },
  },
});
</script>

<style scoped>
.signup-container {
  text-align: center;
  margin: 2rem;
}
</style>