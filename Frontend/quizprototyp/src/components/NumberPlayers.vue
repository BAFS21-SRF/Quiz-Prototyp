<template>
  <div class="">
    <v-card>
      <v-card-title>Anzahl Spieler</v-card-title>
    <v-card-text>
      <v-avatar
      color="#383732"
      size="150"
    >
      <span class="white--text headline">{{ numberPlayers }}</span>
    </v-avatar>
    </v-card-text>
    </v-card>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import axios from 'axios';

@Component
export default class NumberPlayers extends Vue {
  private numberPlayers: string = '';

  private async mounted() {
      // Load all Players from Server
      this.loadNumberPlayers();
      setInterval(() => {
          this.loadNumberPlayers();
      }, 5000);
  }
  private async loadNumberPlayers() {
    const response = await axios.get('http://' + window.location.hostname + ':8888/api/game');
    this.numberPlayers = (response.data.length).toString();
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
