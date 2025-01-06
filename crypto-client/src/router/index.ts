import { createRouter, createWebHistory } from "vue-router";
import Login from "../views/Login.vue";
import SignUp from "../views/SignUp.vue";
import Wallet from "../views/Wallet.vue";

const routes = [
  { path: "/Login", name: "Login", component: Login },
  { path: "/signup", name: "SignUp", component: SignUp },
  { path: "/wallet", name: "Wallet", component: Wallet },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;