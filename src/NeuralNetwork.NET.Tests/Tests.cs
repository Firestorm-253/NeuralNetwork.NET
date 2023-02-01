using NeuralNetwork.NET.Indexes;

namespace NeuralNetwork.NET.Tests;

[TestClass]
public class Tests
{
    const int randomSeed = 605013250;

    [TestMethod]
    public void Test1()
    {
        //# Seed Randomizer
        GlobalRandom = new Random(randomSeed);

        //# Initialize
        var model = new NeuralNetwork();
        
        //# InputLayers
        model.Add(1, 0, Activation.Identity);
        model.Add(2, 1, Activation.Sigmoid);

        //# HiddenLayers
        model.Add(1, 2, Activation.LeakyRelu, 0, 1);

        //# OutputLayers
        model.Add(1, 3, Activation.LeakyRelu, 2);
        model.Add(2, 4, Activation.LeakyRelu, 2);

        //# Compile
        model.Randomize();

        //# Test
        var data = new(LayerIndex, double[])[]
        {
            (0, new double[] { 2.6 }),
            (1, new double[] { 21, -30 }),
        };

        var results = model.Evaluate(data, 3, 4);
    }

    [TestMethod]
    public void SaveLoad()
    {
        //# Seed Randomizer
        GlobalRandom = new Random(randomSeed);

        //# Initialize
        var model = new NeuralNetwork();

        //# InputLayers
        model.Add(6, 0, Activation.Sigmoid);
        model.Add(6, 1, Activation.Sigmoid);

        //# HiddenLayers
        model.Add(20, 2, Activation.LeakyRelu, 0, 1);
        model.Add(20, 3, Activation.LeakyRelu, 2);

        //# OutputLayers
        model.Add(10, 4, Activation.Sigmoid, 3);

        //# Compile
        model.Randomize();

        //# Test
        const string name = "test-network";
        model.Save(name, SaveType.Binary);
        model.Save(name, SaveType.Json);

        var binaryLoaded = new NeuralNetwork($"{name}.nn");
        var jsonLoaded = new NeuralNetwork($"{name}.json");

        Assert.AreEqual(binaryLoaded, jsonLoaded);
        Assert.AreEqual(model, binaryLoaded);
    }
}