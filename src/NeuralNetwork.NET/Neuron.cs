﻿using NeuralNetwork.NET.Indexes;

namespace NeuralNetwork.NET;

public class Neuron
{
    public NeuronIndex NeuronIndex { get; init; }
    public Activation Activation { get; init; }

    public double Blank { get; set; }

    public bool CalculationNeeded { get; set; }
    private double _value;
    public double Value
    {
        get => this.CalculationNeeded ? this.CalculateValue() : this._value;
        set => this._value = value;
    }

    public bool IsWorking => (this.Connections.Count != 0);

    public Neuron(NeuronIndex neuronIndex, Activation activation)
    {
        this.NeuronIndex = neuronIndex;
        this.Activation = activation;
    }

    public double CalculateValue()
    {
        this.CalculationNeeded = false;

        double sum = 0;

        // ToDo: sum all connections

        return this.Set(sum);
    }

    private double Set(double blank)
    {
        this.Blank = blank;
        return this.Value = this.Blank.Activate(this.Activation);
    }

    public void Feed(double blank)
    {
        if (this.IsWorking)
        {
            throw new Exception("ERROR: Can't feed a working neuron!");
        }

        this.Set(blank);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Neuron neuron)
        {
            return false;
        }

        return this.NeuronIndex.Equals(neuron.NeuronIndex);
    }
    public override int GetHashCode()
    {
        return this.NeuronIndex.GetHashCode();
    }
}
