<template>
  <div id="app">
    <header>
      <nav>
        <router-link to="/">Login</router-link>
        <router-link to="/signup">Sign Up</router-link>
        <router-link to="/wallet">Wallet</router-link>
      </nav>
    </header>
    <main>
      <!-- Render the current route's component -->
      <router-view />
    </main>
  </div>
</template>

<script lang="ts">
export default {
  name: "App",
};
</script>

<style>
/* Add some basic styling for navigation */
nav {
  display: flex;
  gap: 1rem;
  background-color: #f4f4f4;
  padding: 1rem;
}

nav a {
  text-decoration: none;
  color: #333;
  font-weight: bold;
}

nav a.router-link-active {
  color: #007bff;
}

main {
  padding: 2rem;
}
</style>