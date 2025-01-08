import { createRouter, createWebHistory } from "vue-router";
import Login from "../components/Login.vue";
import SignUp from "../components/SignUp.vue";
import Wallet from "../components/Wallet.vue";

const routes = [
  { path: "/", name: "Login", component: Login },
  { path: "/signup", name: "SignUp", component: SignUp },
  { path: "/wallet", name: "Wallet", component: Wallet },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;