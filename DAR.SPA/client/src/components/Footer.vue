<template>
  <v-footer dark padless>
    <v-card class="flex" flat tile>
      <v-card-title class="teal">
        <strong class="subheading">My socials</strong>
        <v-spacer></v-spacer>

        <v-btn
          v-for="icon in icons"
          :key="icon"
          class="mx-4"
          dark
          icon
          @click.middle="redirectNewTab(icon)"
          @click="redirect(icon)"
        >
          <v-icon size="24px">{{ icon }}</v-icon>
        </v-btn>
      </v-card-title>

      <v-card-text class="py-2 white--text text-center">
        {{ new Date().getFullYear() }} —
        <strong>Karol Grzesiak</strong>
      </v-card-text>
    </v-card>
  </v-footer>
</template
>
<script>
export default {
  data: () => ({
    icons: ["mdi-github-circle", "mdi-linkedin"]
  }),
  methods: {
    redirect(icon) {
      if (icon == "mdi-github-circle")
        window.open("https://github.com/karolgrzesiak", "_self");
      else window.open("https://www.linkedin.com/in/karol-grzesiak/", "_self");
    },
    redirectNewTab(icon) {
      if (icon == "mdi-github-circle")
        window.open("https://github.com/karolgrzesiak");
      else window.open("https://www.linkedin.com/in/karol-grzesiak/");
    }
  }
};
</script>

